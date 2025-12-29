# TAML .NET

.NET parser and serializer for TAML (Tab Accessible Markup Language).

## Installation

### Via NuGet Package Manager

```bash
dotnet add package TAML.Core
```

For ASP.NET Core configuration integration:

```bash
dotnet add package TAML.Configuration
```

### Building from Source

```bash
cd dotnet
dotnet restore dotnet.sln
dotnet build dotnet.sln --configuration Release
```

## Libraries

This folder contains two main libraries:

### TAML.Core

Core parsing and serialization functionality for TAML documents.

- **TamlDocument**: Main document class for working with TAML data
- **TamlSerializer**: Low-level serialization and deserialization
- **TamlConverter**: Type conversion utilities
- **TamlValidator**: TAML format validation

### TAML.Configuration

Integration with Microsoft.Extensions.Configuration for ASP.NET Core applications.

- Load TAML files as configuration sources
- Full support for the .NET configuration system
- Drop-in replacement for JSON/XML configuration files

## Usage

### Parsing TAML

```csharp
using TAML.Core;

var tamlText = @"
application	MyApp
version	1.0.0

server
	host	localhost
	port	8080
	ssl	true

features
	authentication
	logging
	caching
";

// Parse using TamlDocument
var document = TamlDocument.Parse(tamlText);

// Access values
var appName = document.GetValue<string>("application");  // "MyApp"
var port = document.GetValue<int>("server:port");        // 8080 (flattened key)

// Get nested sections
var serverSection = document.GetSection("server");
var host = serverSection?.GetValue<string>("host");      // "localhost"
```

### Serializing to TAML

```csharp
using TAML.Core;

var data = new Dictionary<string, object?>
{
	["application"] = "MyApp",
	["version"] = "1.0.0",
	["server"] = new Dictionary<string, object?>
	{
		["host"] = "localhost",
		["port"] = 8080,
		["ssl"] = true
	},
	["features"] = new List<string> { "authentication", "logging", "caching" }
};

// Serialize to TAML string
var tamlText = TamlSerializer.Serialize(data);
Console.WriteLine(tamlText);

// Or use TamlDocument
var document = new TamlDocument(data);
document.SaveToFile("config.taml");
```

### Using TamlDocument

```csharp
using TAML.Core;

// Create a new document
var doc = new TamlDocument();

// Set values
doc.SetValue("application", "MyApp");
doc.SetValue("version", "1.0.0");

// Work with nested structures
var serverData = new Dictionary<string, object?>
{
	["host"] = "localhost",
	["port"] = 8080
};
doc.SetValue("server", serverData);

// Access values with indexer
var appName = doc["application"];  // "MyApp"

// Get typed values
var port = doc.GetValue<int>("port");  // with default handling

// Check for keys
if (doc.ContainsKey("server"))
{
	var server = doc.GetSection("server");
	// work with server section
}

// Load from file
var config = TamlDocument.LoadFromFile("appsettings.taml");

// Save to file
doc.SaveToFile("output.taml");
```

### ASP.NET Core Configuration

```csharp
using TAML.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// In your Program.cs or Startup.cs
var builder = WebApplication.CreateBuilder(args);

// Add TAML configuration (looks for appsettings.taml)
builder.Configuration.AddTamlConfiguration();

// Or specify a custom path
builder.Configuration.AddTamlConfiguration("config/myconfig.taml");

var app = builder.Build();

// Access configuration values
var appName = builder.Configuration["application"];
var port = builder.Configuration["server:port"];
var sslEnabled = builder.Configuration.GetValue<bool>("server:ssl");
```

Example TAML configuration file (`appsettings.taml`):

```taml
application	MyWebApp
version	1.0.0

logging
	LogLevel
		Default	Information
		Microsoft.AspNetCore	Warning

AllowedHosts	*

ConnectionStrings
	DefaultConnection	Server=localhost;Database=myapp;
```

### Special Values

TAML distinguishes between null values and empty strings:

```csharp
using TAML.Core;

var tamlText = @"
username	alice
password	~
nickname	""""
";

var doc = TamlDocument.Parse(tamlText);

var username = doc.GetValue<string>("username");  // "alice"
var password = doc.GetValue<string>("password");  // null
var nickname = doc.GetValue<string>("nickname");  // "" (empty string)
```

- Use `~` for null values
- Use `""` for empty strings
- Regular values need no quotes

## Features

- **Simple API**: Parse with `TamlDocument.Parse()`, serialize with `TamlSerializer.Serialize()`
- **Type Conversion**: Automatic conversion of numbers and booleans
- **Null Support**: Use `~` for null values
- **Empty Strings**: Use `""` for empty strings
- **Configuration Integration**: Native support for Microsoft.Extensions.Configuration
- **File Operations**: Load and save TAML files with `LoadFromFile()` and `SaveToFile()`
- **Async Support**: Async file operations with `LoadFromFileAsync()` and `SaveToFileAsync()`
- **Flattening**: Convert nested structures to flat dictionaries for configuration providers

## Validation

The parser validates TAML structure according to the specification:

- Only tabs for indentation (no spaces)
- No tabs in keys or values
- Proper indentation levels
- Valid parent-child relationships

## Building and Testing

### Build

```bash
cd dotnet
dotnet build dotnet.sln --configuration Release
```

### Run Tests

```bash
cd dotnet
dotnet test TAML.Tests/TAML.Tests.csproj
```

## Requirements

- .NET 10.0 or later

## License

MIT
