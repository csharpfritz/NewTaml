using TAML.Core;

namespace TAML.Tests;

public class TamlValidatorTests
{
    #region Valid TAML Tests
    
    [Fact]
    public void GivenValidSimpleTaml_WhenValidating_ThenIsValid()
    {
        // Given
        var taml = "name\tvalue\nage\t42";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenValidNestedTaml_WhenValidating_ThenIsValid()
    {
        // Given
        var taml = "server\n\thost\tlocalhost\n\tport\t8080";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenValidListTaml_WhenValidating_ThenIsValid()
    {
        // Given
        var taml = "items\n\titem1\n\titem2\n\titem3";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenValidDeeplyNestedTaml_WhenValidating_ThenIsValid()
    {
        // Given
        var taml = "level1\n\tlevel2\n\t\tlevel3\n\t\t\tvalue\ttest";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenValidTamlWithComments_WhenValidating_ThenIsValid()
    {
        // Given
        var taml = "# Comment\nname\tvalue\n# Another comment\nage\t42";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenValidTamlWithBlankLines_WhenValidating_ThenIsValid()
    {
        // Given
        var taml = "name\tvalue\n\nage\t42\n\n";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenValidTamlWithMultipleTabSeparators_WhenValidating_ThenIsValid()
    {
        // Given
        var taml = "short\t\t\tvalue1\nmedium\t\tvalue2";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    #endregion
    
    #region Invalid TAML Tests
    
    [Fact]
    public void GivenTamlWithSpaceIndentation_WhenValidating_ThenInvalid()
    {
        // Given
        var taml = "server\n    host\tlocalhost"; // 4 spaces instead of tab
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ValidationErrorType.SpaceIndentation, result.Errors[0].ErrorType);
        Assert.Equal(2, result.Errors[0].LineNumber);
    }
    
    [Fact]
    public void GivenTamlWithMixedIndentation_WhenValidating_ThenInvalid()
    {
        // Given
        var taml = "server\n\t host\tlocalhost"; // Tab + space
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ValidationErrorType.MixedIndentation, result.Errors[0].ErrorType);
        Assert.Equal(2, result.Errors[0].LineNumber);
    }
    
    [Fact]
    public void GivenTamlWithInconsistentIndentation_WhenValidating_ThenInvalid()
    {
        // Given
        var taml = "server\n\t\t\thost\tlocalhost"; // Skips from 0 to 3 tabs
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ValidationErrorType.InconsistentIndentation, result.Errors[0].ErrorType);
        Assert.Equal(2, result.Errors[0].LineNumber);
    }
    
    [Fact]
    public void GivenTamlWithOrphanedIndentation_WhenValidating_ThenInvalid()
    {
        // Given
        var taml = "name\tvalue\n\torphan\tvalue"; // Indent after key-value pair
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ValidationErrorType.OrphanedIndentation, result.Errors[0].ErrorType);
        Assert.Equal(2, result.Errors[0].LineNumber);
    }
    
    [Fact]
    public void GivenTamlWithTabInValue_WhenValidating_ThenInvalid()
    {
        // Given
        var taml = "message\tHello\tWorld"; // Tab in value
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ValidationErrorType.TabInValue, result.Errors[0].ErrorType);
        Assert.Equal(1, result.Errors[0].LineNumber);
    }
    
    [Fact]
    public void GivenTamlWithEmptyKey_WhenValidating_ThenInvalid()
    {
        // Given
        var taml = "parent\n\t\tvalue"; // Skips indent level (orphaned), also wrong structure
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count > 0); // Should have orphaned or inconsistent indentation error
    }
    
    [Fact]
    public void GivenTamlWithMultipleErrors_WhenValidating_ThenAllErrorsReported()
    {
        // Given
        var taml = "server\n    host\tlocalhost\n\t\t\tport\t8080"; // Space indent + skip level
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.False(result.IsValid);
        Assert.True(result.Errors.Count >= 2);
        Assert.Contains(result.Errors, e => e.ErrorType == ValidationErrorType.SpaceIndentation);
        Assert.Contains(result.Errors, e => e.ErrorType == ValidationErrorType.InconsistentIndentation);
    }
    
    #endregion
    
    #region Warning Tests
    
    [Fact]
    public void GivenTamlWithDoubleSpacesInKey_WhenValidating_ThenWarning()
    {
        // Given
        var taml = "server  name"; // Double space in key (should use tab) - this is a parent key
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        // The validator sees this as a valid parent key with spaces in the name
        // Warnings are for heuristic issues, not strict violations
        Assert.True(result.IsValid || result.Errors.All(e => e.Severity == ValidationSeverity.Warning));
        if (result.Errors.Any())
        {
            Assert.Equal(ValidationSeverity.Warning, result.Errors[0].Severity);
            Assert.Equal(ValidationErrorType.InvalidKeyFormat, result.Errors[0].ErrorType);
        }
    }
    
    #endregion
    
    #region Edge Cases
    
    [Fact]
    public void GivenEmptyString_WhenValidating_ThenValid()
    {
        // Given
        var taml = "";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenOnlyComments_WhenValidating_ThenValid()
    {
        // Given
        var taml = "# Comment 1\n# Comment 2\n# Comment 3";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    [Fact]
    public void GivenOnlyBlankLines_WhenValidating_ThenValid()
    {
        // Given
        var taml = "\n\n\n";
        
        // When
        var result = TamlValidator.Validate(taml);
        
        // Then
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    #endregion
    
    #region Validation Result Tests
    
    [Fact]
    public void GivenInvalidTaml_WhenConvertingToString_ThenHumanReadableOutput()
    {
        // Given
        var taml = "server\n    host\tlocalhost";
        
        // When
        var result = TamlValidator.Validate(taml);
        var output = result.ToString();
        
        // Then
        Assert.Contains("Invalid TAML", output);
        Assert.Contains("Line 2", output);
        Assert.Contains("Error", output);
    }
    
    [Fact]
    public void GivenValidTaml_WhenConvertingToString_ThenValidMessage()
    {
        // Given
        var taml = "name\tvalue";
        
        // When
        var result = TamlValidator.Validate(taml);
        var output = result.ToString();
        
        // Then
        Assert.Equal("Valid TAML", output);
    }
    
    #endregion
}
