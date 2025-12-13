# Installation Guide

## Quick Install

### Option 1: Install from VSIX (Recommended)

1. **Build the extension:**
   ```bash
   cd tools/vscode-taml
   npm install -g vsce
   vsce package
   ```
   This creates a `taml-0.1.0.vsix` file.

2. **Install in VSCode:**
   - Open VSCode
   - Press `Ctrl+Shift+P` (or `Cmd+Shift+P` on Mac)
   - Type "Install from VSIX"
   - Select the `taml-0.1.0.vsix` file

3. **Verify installation:**
   - Open any `.taml` file
   - Syntax highlighting should activate automatically

### Option 2: Development Mode

For testing during development:

1. **Open extension folder:**
   ```bash
   cd tools/vscode-taml
   code .
   ```

2. **Run extension:**
   - Press `F5` to open a new VSCode window with the extension loaded
   - Open a `.taml` file in the new window
   - Test syntax highlighting

### Option 3: Manual Installation

1. **Copy to extensions folder:**

   **Windows:**
   ```bash
   xcopy /E /I tools\vscode-taml %USERPROFILE%\.vscode\extensions\taml-0.1.0
   ```

   **macOS/Linux:**
   ```bash
   cp -r tools/vscode-taml ~/.vscode/extensions/taml-0.1.0
   ```

2. **Reload VSCode:**
   - Press `Ctrl+Shift+P` (or `Cmd+Shift+P`)
   - Type "Reload Window"
   - Press Enter

## Prerequisites

To build the extension (optional):

- **Node.js** 16 or higher
- **npm** (comes with Node.js)
- **vsce** (Visual Studio Code Extension Manager)

Install vsce globally:
```bash
npm install -g vsce
```

## Testing the Extension

1. Create a test file `test.taml`:
   ```taml
   # Test TAML file
   application		MyApp
   version			1.0.0
   
   server
   	host		localhost
   	port		3000
   	enabled		true
   
   features
   	feature_one
   	feature_two
   	feature_three
   ```

2. Open in VSCode
3. Verify syntax highlighting:
   - Comments should be dimmed/green
   - Keys should be colored (blue/cyan)
   - Values should be colored based on type
   - Booleans like `true` should be highlighted
   - Numbers should be distinct

## Troubleshooting

### Extension Not Activating

**Problem:** Opening `.taml` files doesn't trigger syntax highlighting.

**Solution:**
1. Check file extension is exactly `.taml`
2. Reload window: `Ctrl+Shift+P` → "Reload Window"
3. Verify extension is installed: Check Extensions view

### Wrong Colors

**Problem:** Syntax highlighting colors don't match examples.

**Solution:**
- Colors come from your VSCode theme
- Try different themes to see variations
- The extension assigns scopes; themes assign colors

### Tabs Not Working

**Problem:** VSCode inserts spaces instead of tabs.

**Solution:**
1. Check bottom-right status bar
2. Click where it says "Spaces: X"
3. Select "Indent Using Tabs"
4. Or add to settings.json:
   ```json
   {
     "[taml]": {
       "editor.insertSpaces": false
     }
   }
   ```

### Indentation Not Folding

**Problem:** Can't fold sections by indentation.

**Solution:**
1. Ensure "Editor: Folding Strategy" is set to "auto" or "indentation"
2. Settings → Search "folding strategy"
3. Or in settings.json:
   ```json
   {
     "editor.foldingStrategy": "indentation"
   }
   ```

## Uninstalling

1. Go to Extensions view (`Ctrl+Shift+X`)
2. Find "TAML" extension
3. Click gear icon → "Uninstall"

Or via command line:
```bash
code --uninstall-extension taml-lang.taml
```

## Publishing (For Maintainers)

To publish to VSCode Marketplace:

1. **Create Publisher:**
   ```bash
   vsce create-publisher your-publisher-name
   ```

2. **Get Personal Access Token:**
   - Go to https://dev.azure.com
   - Create a PAT with Marketplace (Publish) scope

3. **Login:**
   ```bash
   vsce login your-publisher-name
   ```

4. **Publish:**
   ```bash
   vsce publish
   ```

## Support

If you encounter issues:
1. Check [GitHub Issues](https://github.com/your-repo/taml/issues)
2. Read the [TAML Specification](../../TAML-SPEC.md)
3. Look at [examples](../../examples/)
4. Open a new issue with details

---

Happy TAML editing! 🎨✨
