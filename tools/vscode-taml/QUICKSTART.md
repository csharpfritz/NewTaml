# TAML VSCode Extension - Quick Start

## 🚀 Get Started in 3 Steps

### Step 1: Build the Extension

```bash
# Install VSCE (first time only)
npm install -g vsce

# Navigate to extension folder
cd tools/vscode-taml

# Build it!
vsce package
```

This creates `taml-0.1.0.vsix`

### Step 2: Install in VSCode

```bash
code --install-extension taml-0.1.0.vsix
```

Or use the GUI:
- Open VSCode Extensions (`Ctrl+Shift+X`)
- Click `...` menu → "Install from VSIX..."
- Select `taml-0.1.0.vsix`

### Step 3: Test It

1. Open VSCode
2. Create a file: `test.taml`
3. Type some TAML:

```taml
# My Config
application		MyApp
version			1.0.0

server
	host		localhost
	port		3000
	enabled		true
```

4. **See the magic!** 🎨
   - Comments are highlighted
   - Keys are colored
   - Values show type-based colors
   - Booleans like `true` stand out

## 🎯 Features at a Glance

✅ **Syntax Highlighting**
- Comments: `# like this`
- Keys: Everything before tabs
- Values: Everything after tabs
- Booleans: `true`, `false`, `yes`, `no`
- Numbers: `42`, `3.14`, `-100`
- Null: `null`, `nil`
- Env vars: `${VAR_NAME}`

✅ **Smart Editing**
- Tab key inserts tabs (not spaces)
- Comment toggle: `Ctrl+/`
- Code folding by indentation
- Auto-configured for TAML

✅ **Works with All Themes**
- Dark+
- Light+
- Monokai
- Solarized
- Dracula
- One Dark Pro
- ...and more!

## 🧪 Try the Test File

We included a test file with all features:

```bash
# In VSCode
File → Open File → tools/vscode-taml/test.taml
```

This file demonstrates:
- Comments
- Key-value pairs
- Nested structures
- Lists
- All value types
- Tab alignment

## 🎨 Customize Colors (Optional)

TAML works with your theme, but you can customize:

1. Open VSCode Settings (`Ctrl+,`)
2. Search for "Token Color Customizations"
3. Add custom colors:

```json
{
  "editor.tokenColorCustomizations": {
    "textMateRules": [
      {
        "scope": "entity.name.tag.taml",
        "settings": {
          "foreground": "#4EC9B0",
          "fontStyle": "bold"
        }
      },
      {
        "scope": "constant.language.boolean.taml",
        "settings": {
          "foreground": "#569CD6"
        }
      }
    ]
  }
}
```

## 📖 Need Help?

- **Installation Issues**: See [INSTALL.md](./INSTALL.md)
- **Build Problems**: See [BUILD.md](./BUILD.md)
- **Full Documentation**: See [README.md](./README.md)
- **TAML Spec**: See [../../TAML-SPEC.md](../../TAML-SPEC.md)
- **Examples**: See [../../examples/](../../examples/)

## 🐛 Troubleshooting

### Not highlighting?
1. Check file extension is `.taml`
2. Reload window: `Ctrl+Shift+P` → "Reload Window"

### Inserting spaces instead of tabs?
1. Check status bar (bottom right)
2. Click "Spaces: X"
3. Select "Indent Using Tabs"

### Can't fold sections?
1. Hover over line numbers
2. Click fold icon (▼)
3. Or use `Ctrl+Shift+[` to fold

## 🎉 That's It!

You're ready to edit TAML files with beautiful syntax highlighting!

Open any `.taml` file and enjoy the visual clarity.

**TAML: Because sometimes less markup is more.** ✨

---

## Next Steps

1. ✅ Install extension
2. ✅ Test with `test.taml`
3. 📝 Edit real TAML files
4. 🎨 Try different themes
5. 🚀 Build something awesome!

Check out the [examples](../../examples/) directory for inspiration:
- Web app configs
- API documentation
- Cloud infrastructure
- Game levels
- Recipes
- Team directories
