# TAML Language Server - Quick Start

## 🚀 Get Real-Time Validation in 5 Minutes!

### Step 1: Install Dependencies

```bash
cd tools/taml-language-server
npm install
```

This installs all needed packages for client and server.

### Step 2: Compile TypeScript

```bash
npm run compile
```

This compiles the language server and client code.

### Step 3: Package Extension

```bash
npm install -g vsce  # If not already installed
npm run package
```

Creates: `taml-language-server-0.1.0.vsix`

### Step 4: Install in VSCode

```bash
code --install-extension taml-language-server-0.1.0.vsix
```

### Step 5: Test It!

1. Open VSCode
2. Create a file: `test.taml`
3. Type some invalid TAML:

```taml
server
    host	localhost    # ❌ Space indentation - see red squiggle!
```

4. **Watch the magic!** Red squiggles appear instantly! 🎉

## What You Get

### ✅ Real-Time Error Detection
- **Space indentation** - Instant red squiggle
- **Mixed tabs/spaces** - Caught immediately  
- **Orphaned indentation** - Can't miss it
- **Tabs in values** - Highlighted right away
- **Invalid structure** - Shows line-by-line

### ⚡ Live Feedback
- Errors appear **as you type**
- No need to save or run commands
- Hover over errors for details
- See all problems in Problems panel

### ⚙️ Configurable
Add to `settings.json`:
```json
{
  "taml.validation.enable": true,
  "taml.validation.showWarnings": true
}
```

## Example Errors

Try these to see the validator in action:

### Space Indentation (Error)
```taml
server
    host	localhost    # ❌ Red squiggle on spaces
```

### Tab in Value (Error)
```taml
message	Hello	World    # ❌ Red squiggle on second tab
```

### Orphaned Line (Error)
```taml
name	value
	orphan	bad    # ❌ Red squiggle - no parent
```

### Double Spaces (Warning)
```taml
server  name    # ⚠️ Yellow squiggle - might need tab
```

## Development Mode

Want to modify the validator?

```bash
# Open in VSCode
cd tools/taml-language-server
code .

# Start watch mode (auto-recompile on save)
npm run watch

# Press F5 to launch Extension Development Host
# Make changes to server/src/server.ts
# Reload window to see changes
```

## Troubleshooting

### Not seeing errors?

1. Check file has `.taml` extension
2. Reload window: `Ctrl+Shift+P` → "Reload Window"
3. Check settings: validation.enable should be `true`

### Extension not activating?

1. Check Output panel → "TAML Language Server"
2. Verify compiled files exist:
   - `client/out/extension.js`
   - `server/out/server.js`
3. Rebuild: `npm run compile`

### Wrong errors showing?

1. Check you're using tabs, not spaces for indentation
2. VSCode might show tabs as spaces visually
3. Check bottom-right status bar - should say "Tab Size: 1"

## Next Steps

1. ✅ Install language server
2. ✅ Test with invalid TAML
3. 📝 Edit real TAML files with confidence!
4. 🎨 Combine with syntax highlighter for full experience
5. 🚀 Never write invalid TAML again!

## Full Documentation

- **Installation**: [BUILD.md](./BUILD.md)
- **Features**: [README.md](./README.md)
- **Specification**: [../../TAML-SPEC.md](../../TAML-SPEC.md)

---

**Real-time TAML validation powered by LSP!** ⚡✨
