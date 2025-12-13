# TAML Language Server

A **Language Server Protocol (LSP)** implementation for TAML that provides real-time validation, diagnostics, and error checking in Visual Studio Code.

## Features

### 🔴 Real-Time Error Detection
Get instant feedback with red squiggles for:
- **Space indentation** - Must use tabs, not spaces
- **Mixed indentation** - No mixing tabs and spaces
- **Inconsistent indentation** - Can't skip indent levels
- **Orphaned lines** - Indented lines need a parent
- **Tabs in values** - Values can't contain tabs
- **Empty keys** - All lines need content

### ⚠️ Warnings
Helpful hints for potential issues:
- **Double spaces in keys** - Might have meant to use tabs

### ⚙️ Configuration
Customize validation behavior:
- Enable/disable validation
- Show/hide warnings
- Debug server communication

## Installation

### Prerequisites
- Node.js 16 or higher
- Visual Studio Code 1.75 or higher
- The TAML syntax highlighting extension

### Quick Install

```bash
cd tools/taml-language-server

# Install dependencies
npm install

# Compile TypeScript
npm run compile

# Package extension
npm run package

# Install in VSCode
code --install-extension taml-language-server-*.vsix
```

## Usage

Once installed, the language server automatically activates when you open a `.taml` file.

### Error Detection

Open a TAML file and try introducing errors:

```taml
# This will show errors:
server
    host	localhost    # ❌ Space indentation
	port	8080
```

```taml
# This will show errors:
name	Hello	World    # ❌ Tab in value
```

```taml
# This will show errors:
server
			port	8080    # ❌ Skipped indentation level
```

### Configuration

Add to your VSCode `settings.json`:

```json
{
  "taml.validation.enable": true,
  "taml.validation.showWarnings": true,
  "taml.trace.server": "off"
}
```

#### Settings

| Setting | Default | Description |
|---------|---------|-------------|
| `taml.validation.enable` | `true` | Enable/disable validation |
| `taml.validation.showWarnings` | `true` | Show warnings in addition to errors |
| `taml.trace.server` | `"off"` | Trace LSP communication (`"off"`, `"messages"`, `"verbose"`) |

## How It Works

### Architecture

```
┌─────────────────┐
│  VSCode Client  │
│  (extension.ts) │
└────────┬────────┘
         │ LSP Protocol
         │ (JSON-RPC)
┌────────▼────────┐
│  Language       │
│  Server         │
│  (server.ts)    │
└────────┬────────┘
         │
┌────────▼────────┐
│  TAML Validator │
│  (TypeScript)   │
└─────────────────┘
```

### Communication Flow

1. **User edits file** → VSCode sends document changes to server
2. **Server validates** → Runs TAML validation rules
3. **Server sends diagnostics** → Error/warning information
4. **VSCode displays** → Red/yellow squiggles in editor

### Validation Rules

The language server implements all TAML validation rules from the spec:

| Rule | Error Type | Severity |
|------|------------|----------|
| Space indentation | `SpaceIndentation` | Error |
| Mixed indentation | `MixedIndentation` | Error |
| Inconsistent levels | `InconsistentIndentation` | Error |
| Orphaned indentation | `OrphanedIndentation` | Error |
| Tab in value | `TabInValue` | Error |
| Empty key | `EmptyKey` | Error |
| Double spaces | `InvalidKeyFormat` | Warning |

## Development

### Setup Development Environment

```bash
# Install dependencies
cd tools/taml-language-server
npm install

# Compile in watch mode
npm run watch
```

### Testing Changes

1. Open `tools/taml-language-server` in VSCode
2. Press `F5` to launch Extension Development Host
3. Open a `.taml` file in the new window
4. Make changes to server code
5. Reload the Extension Development Host window

### Project Structure

```
taml-language-server/
├── package.json              # Extension manifest
├── tsconfig.json             # TypeScript config
├── src/
│   └── extension.ts          # VSCode client
├── server/
│   ├── package.json          # Server dependencies
│   ├── tsconfig.json         # Server TypeScript config
│   └── src/
│       └── server.ts         # Language server implementation
└── README.md                 # This file
```

### Key Files

#### `src/extension.ts`
- VSCode extension entry point
- Starts the language server
- Handles activation/deactivation

#### `server/src/server.ts`
- Language Server Protocol implementation
- Document validation logic
- Diagnostic generation
- Settings management

## Debugging

### Enable Tracing

Set in `settings.json`:
```json
{
  "taml.trace.server": "verbose"
}
```

Then check **Output** → **TAML Language Server** in VSCode.

### Common Issues

**Server not starting**
- Check Output panel for errors
- Verify Node.js is installed: `node --version`
- Rebuild: `npm run compile`

**Diagnostics not showing**
- Check validation is enabled in settings
- Reload VSCode window
- Check file has `.taml` extension

**Wrong diagnostics**
- Update server code in `server/src/server.ts`
- Recompile: `npm run compile`
- Reload Extension Development Host

## Comparison with C# Validator

The TypeScript language server implements the same logic as the C# `TamlValidator`:

| Feature | C# Validator | TypeScript LSP |
|---------|-------------|----------------|
| Space indentation | ✅ | ✅ |
| Mixed indentation | ✅ | ✅ |
| Inconsistent indent | ✅ | ✅ |
| Orphaned indentation | ✅ | ✅ |
| Tab in value | ✅ | ✅ |
| Empty key | ✅ | ✅ |
| Warnings | ✅ | ✅ |
| Real-time | ❌ | ✅ |
| VSCode integration | ❌ | ✅ |

Both use the same validation rules from the TAML specification.

## Future Enhancements

Planned features for future versions:

### Auto-completion
- Key suggestions based on context
- Common value patterns

### Hover Information
- Show value types
- Display validation rules
- Link to documentation

### Code Actions
- Quick fixes for common errors
- Convert spaces to tabs
- Format document

### Formatting
- Align values with multiple tabs
- Normalize indentation
- Sort keys

### Go to Definition
- Navigate to key declarations
- Find all references

## Contributing

Want to improve the language server?

1. **Fix bugs** - Check open issues
2. **Add features** - Implement planned enhancements
3. **Improve diagnostics** - Better error messages
4. **Add tests** - Unit tests for validation logic

## Resources

- [TAML Specification](../../TAML-SPEC.md)
- [C# Validator](../../dotnet/TAML.Core/TamlValidator.cs)
- [LSP Specification](https://microsoft.github.io/language-server-protocol/)
- [VSCode Extension API](https://code.visualstudio.com/api)

## License

MIT License - See [LICENSE](../../LICENSE) file for details.

---

**Real-time validation powered by Language Server Protocol!** 🚀
