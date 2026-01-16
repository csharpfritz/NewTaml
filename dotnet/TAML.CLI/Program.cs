using System.CommandLine;
using TAML.Core;

// Root command
var rootCommand = new RootCommand("TAML CLI - Command-line tool for TAML (Tab Annotated Markup Language)")
{
	Name = "taml"
};

// ============================================
// Convert command
// ============================================
var convertCommand = new Command("convert", "Convert TAML files to other formats");

var inputFileOption = new Option<FileInfo>(
	aliases: ["--input", "-i"],
	description: "Input TAML file to convert")
{ IsRequired = true };

var outputFileOption = new Option<FileInfo?>(
	aliases: ["--output", "-o"],
	description: "Output file (defaults to stdout if not specified)");

var formatOption = new Option<string>(
	aliases: ["--format", "-f"],
	description: "Output format",
	getDefaultValue: () => "yaml");
formatOption.AddCompletions(["yaml", "json"]);

convertCommand.AddOption(inputFileOption);
convertCommand.AddOption(outputFileOption);
convertCommand.AddOption(formatOption);

convertCommand.SetHandler(async (input, output, format) =>
{
	try
	{
		if (!input.Exists)
		{
			Console.Error.WriteLine($"Error: Input file '{input.FullName}' not found.");
			Environment.ExitCode = 1;
			return;
		}

		var tamlContent = await File.ReadAllTextAsync(input.FullName);

		string result = format.ToLowerInvariant() switch
		{
			"yaml" => TamlConverter.ConvertToYaml(tamlContent),
			"json" => TamlConverter.ConvertToJson(tamlContent),
			_ => throw new ArgumentException($"Unsupported format: {format}. Use 'yaml' or 'json'.")
		};

		if (output != null)
		{
			await File.WriteAllTextAsync(output.FullName, result);
			Console.WriteLine($"Successfully converted '{input.Name}' to {format.ToUpperInvariant()} -> '{output.FullName}'");
		}
		else
		{
			Console.WriteLine(result);
		}
	}
	catch (TAMLException ex)
	{
		Console.Error.WriteLine($"TAML Parse Error: {ex.Message}");
		Environment.ExitCode = 1;
	}
	catch (Exception ex)
	{
		Console.Error.WriteLine($"Error: {ex.Message}");
		Environment.ExitCode = 1;
	}
}, inputFileOption, outputFileOption, formatOption);

rootCommand.AddCommand(convertCommand);

// ============================================
// Validate command
// ============================================
var validateCommand = new Command("validate", "Validate TAML documents");

var validateInputOption = new Option<FileInfo>(
	aliases: ["--input", "-i"],
	description: "TAML file to validate")
{ IsRequired = true };

var verboseOption = new Option<bool>(
	aliases: ["--verbose", "-v"],
	description: "Show detailed validation output",
	getDefaultValue: () => false);

validateCommand.AddOption(validateInputOption);
validateCommand.AddOption(verboseOption);

validateCommand.SetHandler(async (input, verbose) =>
{
	try
	{
		if (!input.Exists)
		{
			Console.Error.WriteLine($"Error: Input file '{input.FullName}' not found.");
			Environment.ExitCode = 1;
			return;
		}

		var tamlContent = await File.ReadAllTextAsync(input.FullName);
		var result = TamlValidator.Validate(tamlContent);

		if (result.IsValid)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine($"✓ '{input.Name}' is valid TAML");
			Console.ResetColor();

			if (verbose)
			{
				var lineCount = tamlContent.Split('\n').Length;
				Console.WriteLine($"  Lines: {lineCount}");
			}
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"✗ '{input.Name}' has {result.Errors.Count} validation error(s):");
			Console.ResetColor();

			foreach (var error in result.Errors)
			{
				var color = error.Severity == ValidationSeverity.Error ? ConsoleColor.Red : ConsoleColor.Yellow;
				Console.ForegroundColor = color;
				var severityIcon = error.Severity == ValidationSeverity.Error ? "✗" : "⚠";
				Console.Write($"  {severityIcon} ");
				Console.ResetColor();
				Console.WriteLine($"Line {error.LineNumber}, Column {error.Column}: {error.Message}");

				if (verbose)
				{
					var lines = tamlContent.Split('\n');
					if (error.LineNumber <= lines.Length)
					{
						var line = lines[error.LineNumber - 1];
						Console.ForegroundColor = ConsoleColor.DarkGray;
						Console.WriteLine($"    | {line}");
						Console.ResetColor();
					}
				}
			}

			Environment.ExitCode = 1;
		}
	}
	catch (Exception ex)
	{
		Console.Error.WriteLine($"Error: {ex.Message}");
		Environment.ExitCode = 1;
	}
}, validateInputOption, verboseOption);

rootCommand.AddCommand(validateCommand);

// ============================================
// Info command
// ============================================
var infoCommand = new Command("info", "Display information about a TAML file");

var infoInputOption = new Option<FileInfo>(
	aliases: ["--input", "-i"],
	description: "TAML file to analyze")
{ IsRequired = true };

infoCommand.AddOption(infoInputOption);

infoCommand.SetHandler(async (input) =>
{
	try
	{
		if (!input.Exists)
		{
			Console.Error.WriteLine($"Error: Input file '{input.FullName}' not found.");
			Environment.ExitCode = 1;
			return;
		}

		var tamlContent = await File.ReadAllTextAsync(input.FullName);
		var lines = tamlContent.Split('\n');
		var nonEmptyLines = lines.Count(l => !string.IsNullOrWhiteSpace(l));
		var commentLines = lines.Count(l => l.TrimStart().StartsWith('#'));
		var contentLines = nonEmptyLines - commentLines;

		Console.WriteLine($"File: {input.Name}");
		Console.WriteLine($"Size: {input.Length} bytes");
		Console.WriteLine($"Lines: {lines.Length} total, {contentLines} content, {commentLines} comments");

		var validationResult = TamlValidator.Validate(tamlContent);
		Console.Write("Status: ");
		if (validationResult.IsValid)
		{
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Valid");
		}
		else
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"Invalid ({validationResult.Errors.Count} errors)");
		}
		Console.ResetColor();

		// Try to parse and show structure
		if (validationResult.IsValid)
		{
			try
			{
				var document = TamlDocument.Parse(tamlContent);
				Console.WriteLine($"Root keys: {string.Join(", ", document.GetKeys())}");
			}
			catch
			{
				// Ignore parse errors for info display
			}
		}
	}
	catch (Exception ex)
	{
		Console.Error.WriteLine($"Error: {ex.Message}");
		Environment.ExitCode = 1;
	}
}, infoInputOption);

rootCommand.AddCommand(infoCommand);

// Run the CLI
return await rootCommand.InvokeAsync(args);
