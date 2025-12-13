using System.Text;

namespace TAML.Core;

/// <summary>
/// Validates TAML documents and provides detailed error reporting
/// </summary>
public class TamlValidator
{
    /// <summary>
    /// Validates a TAML string and returns any validation errors
    /// </summary>
    public static ValidationResult Validate(string taml)
    {
        var errors = new List<ValidationError>();
        var lines = taml.Split(new[] { '\n', '\r' }, StringSplitOptions.None);
        
        var previousLineInfo = new LineInfo { IndentLevel = -1, IsParent = false };
        
        for (int i = 0; i < lines.Length; i++)
        {
            var lineNumber = i + 1;
            var line = lines[i];
            
            // Skip blank lines and comments
            if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
            {
                previousLineInfo = new LineInfo { IndentLevel = previousLineInfo.IndentLevel, IsParent = false };
                continue;
            }
            
            var lineInfo = ValidateLine(line, lineNumber, previousLineInfo, errors);
            previousLineInfo = lineInfo;
        }
        
        return new ValidationResult
        {
            IsValid = errors.Count == 0,
            Errors = errors
        };
    }
    
    private static LineInfo ValidateLine(string line, int lineNumber, LineInfo previousLine, List<ValidationError> errors)
    {
        var lineInfo = new LineInfo();
        
        // 1. Check for spaces in indentation
        if (line.Length > 0 && line[0] == ' ')
        {
            errors.Add(new ValidationError
            {
                LineNumber = lineNumber,
                ErrorType = ValidationErrorType.SpaceIndentation,
                Message = "Indentation must use tabs, not spaces",
                Column = 1
            });
            return lineInfo;
        }
        
        // Count leading tabs
        int indentLevel = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] == '\t')
            {
                indentLevel++;
            }
            else if (line[i] == ' ')
            {
                // 2. Check for mixed tabs and spaces
                errors.Add(new ValidationError
                {
                    LineNumber = lineNumber,
                    ErrorType = ValidationErrorType.MixedIndentation,
                    Message = "Mixed spaces and tabs in indentation",
                    Column = i + 1
                });
                return lineInfo;
            }
            else
            {
                break;
            }
        }
        
        lineInfo.IndentLevel = indentLevel;
        
        // Only check indentation rules if we're not on the first content line
        if (previousLine.IndentLevel >= 0)
        {
            // 3. Check for inconsistent indentation (skipping levels)
            if (indentLevel > previousLine.IndentLevel + 1)
            {
                errors.Add(new ValidationError
                {
                    LineNumber = lineNumber,
                    ErrorType = ValidationErrorType.InconsistentIndentation,
                    Message = $"Invalid indentation level (expected max {previousLine.IndentLevel + 1} tabs, found {indentLevel})",
                    Column = 1
                });
            }
            
            // 4. Check for orphaned indentation
            if (indentLevel > previousLine.IndentLevel && !previousLine.IsParent)
            {
                errors.Add(new ValidationError
                {
                    LineNumber = lineNumber,
                    ErrorType = ValidationErrorType.OrphanedIndentation,
                    Message = "Indented line has no parent (previous line was not a parent key)",
                    Column = 1
                });
            }
        }
        
        // Get content after indentation
        var content = line.Substring(indentLevel);
        
        // 5. Check for empty key
        if (string.IsNullOrWhiteSpace(content))
        {
            errors.Add(new ValidationError
            {
                LineNumber = lineNumber,
                ErrorType = ValidationErrorType.EmptyKey,
                Message = "Line has no content after indentation",
                Column = indentLevel + 1
            });
            return lineInfo;
        }
        
        // Find first tab in content
        var firstTabIndex = content.IndexOf('\t');
        
        if (firstTabIndex == -1)
        {
            // This is a parent key or list item
            lineInfo.IsParent = true;
            
            // Check for invalid characters in key
            if (content.Contains("  ")) // Double space might indicate intent for separation
            {
                errors.Add(new ValidationError
                {
                    LineNumber = lineNumber,
                    ErrorType = ValidationErrorType.InvalidKeyFormat,
                    Message = "Key contains multiple spaces (did you mean to use tabs?)",
                    Column = indentLevel + content.IndexOf("  ") + 1,
                    Severity = ValidationSeverity.Warning
                });
            }
        }
        else if (firstTabIndex == 0)
        {
            // 6. Empty key (line starts with tab after indentation)
            errors.Add(new ValidationError
            {
                LineNumber = lineNumber,
                ErrorType = ValidationErrorType.EmptyKey,
                Message = "Key is empty (line starts with tab)",
                Column = indentLevel + 1
            });
            return lineInfo;
        }
        else
        {
            // This is a key-value pair
            var key = content.Substring(0, firstTabIndex);
            
            // 7. Check for tabs in key
            // (Already handled by firstTabIndex logic, but check for clarity)
            
            // Skip all separator tabs
            int valueStart = firstTabIndex;
            while (valueStart < content.Length && content[valueStart] == '\t')
            {
                valueStart++;
            }
            
            if (valueStart < content.Length)
            {
                var value = content.Substring(valueStart);
                
                // 8. Check for tabs in value
                if (value.Contains('\t'))
                {
                    var tabInValueIndex = value.IndexOf('\t');
                    errors.Add(new ValidationError
                    {
                        LineNumber = lineNumber,
                        ErrorType = ValidationErrorType.TabInValue,
                        Message = "Value contains invalid tab character",
                        Column = indentLevel + firstTabIndex + (valueStart - firstTabIndex) + tabInValueIndex + 1
                    });
                }
            }
            
            lineInfo.IsParent = false;
        }
        
        return lineInfo;
    }
    
    private class LineInfo
    {
        public int IndentLevel { get; set; }
        public bool IsParent { get; set; }
    }
}

/// <summary>
/// Result of TAML validation
/// </summary>
public class ValidationResult
{
    public bool IsValid { get; set; }
    public List<ValidationError> Errors { get; set; } = new();
    
    public override string ToString()
    {
        if (IsValid)
            return "Valid TAML";
        
        var sb = new StringBuilder();
        sb.AppendLine($"Invalid TAML: {Errors.Count} error(s) found");
        foreach (var error in Errors)
        {
            sb.AppendLine(error.ToString());
        }
        return sb.ToString();
    }
}

/// <summary>
/// A validation error in a TAML document
/// </summary>
public class ValidationError
{
    public int LineNumber { get; set; }
    public int Column { get; set; }
    public ValidationErrorType ErrorType { get; set; }
    public string Message { get; set; } = string.Empty;
    public ValidationSeverity Severity { get; set; } = ValidationSeverity.Error;
    
    public override string ToString()
    {
        var severity = Severity == ValidationSeverity.Error ? "Error" : "Warning";
        return $"Line {LineNumber}, Column {Column}: {severity}: {Message}";
    }
}

/// <summary>
/// Types of validation errors
/// </summary>
public enum ValidationErrorType
{
    SpaceIndentation,
    MixedIndentation,
    InconsistentIndentation,
    OrphanedIndentation,
    TabInKey,
    TabInValue,
    EmptyKey,
    InvalidKeyFormat,
    ParentWithValue
}

/// <summary>
/// Severity of validation errors
/// </summary>
public enum ValidationSeverity
{
    Warning,
    Error
}
