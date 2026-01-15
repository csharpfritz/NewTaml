# TAML CLI

A command-line tool for working with TAML (Tab Annotated Markup Language) files.

## Features

- **Convert** TAML files to YAML or JSON format
- **Validate** TAML documents with detailed error reporting
- **Info** display file statistics and structure

## Installation

### Build from Source

```bash
cd dotnet
dotnet build TAML.CLI/TAML.CLI.csproj -c Release
```

### Install as Global Tool (Optional)

```bash
dotnet pack TAML.CLI/TAML.CLI.csproj -c Release
dotnet tool install --global --add-source ./TAML.CLI/bin/Release taml
```

## Usage

### Convert TAML to Other Formats

Convert TAML to YAML (default):
```bash
taml convert -i config.taml
```

Convert TAML to JSON:
```bash
taml convert -i config.taml -f json
```

Save output to a file:
```bash
taml convert -i config.taml -o config.yaml
taml convert -i config.taml -f json -o config.json
```

### Validate TAML Documents

Basic validation:
```bash
taml validate -i config.taml
```

Verbose output with line context:
```bash
taml validate -i config.taml -v
```

Example output for invalid file:
```
✗ 'invalid.taml' has 2 validation error(s):
  ✗ Line 3, Column 1: Indentation must use tabs, not spaces
    |   invalid_spaces	value
  ✗ Line 5, Column 1: Invalid indentation level (expected max 1 tabs, found 2)
    | 		double_indent	bad
```

### Display File Information

```bash
taml info -i config.taml
```

Example output:
```
File: web-app-config.taml
Size: 1493 bytes
Lines: 93 total, 82 content, 2 comments
Status: Valid
Root keys: application, version, environment, server, database, cache, auth, features, monitoring, logging
```

## Command Reference

### Global Options

| Option | Description |
|--------|-------------|
| `--version` | Show version information |
| `-h, --help` | Show help and usage information |

### convert

Convert TAML files to other formats.

| Option | Description |
|--------|-------------|
| `-i, --input <file>` | (Required) Input TAML file to convert |
| `-o, --output <file>` | Output file (defaults to stdout) |
| `-f, --format <yaml\|json>` | Output format (default: yaml) |

### validate

Validate TAML documents.

| Option | Description |
|--------|-------------|
| `-i, --input <file>` | (Required) TAML file to validate |
| `-v, --verbose` | Show detailed output with line context |

### info

Display information about a TAML file.

| Option | Description |
|--------|-------------|
| `-i, --input <file>` | (Required) TAML file to analyze |

## Exit Codes

| Code | Description |
|------|-------------|
| 0 | Success |
| 1 | Error (file not found, parse error, validation failed) |

## Validation Rules

The CLI validates TAML files against these rules:

- ✅ Indentation must use tabs, not spaces
- ✅ No mixed tabs and spaces in indentation
- ✅ Consistent indentation levels (max 1 level increase)
- ✅ Indented lines must have a parent key
- ✅ Keys cannot be empty
- ✅ Values cannot contain tab characters
- ✅ Quotes are only allowed as `""` for empty strings

## Examples

### Converting a Configuration File

```bash
# Convert TAML config to YAML
taml convert -i app-config.taml -o app-config.yaml

# Convert to JSON for API consumption
taml convert -i app-config.taml -f json -o app-config.json
```

### CI/CD Integration

```bash
# Validate all TAML files in a directory
for file in configs/*.taml; do
    taml validate -i "$file" || exit 1
done
```

### Pre-commit Hook

```bash
#!/bin/bash
# .git/hooks/pre-commit
for file in $(git diff --cached --name-only | grep '\.taml$'); do
    taml validate -i "$file" || exit 1
done
```

## Requirements

- .NET 10.0 or later

## License

See the project root LICENSE file.
