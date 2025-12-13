# Building the TAML Language Server

## Quick Build

```bash
cd tools/taml-language-server

# Install all dependencies
npm install

# Compile TypeScript
npm run compile

# Package extension
npm run package
```

This creates `taml-language-server-0.1.0.vsix`

## Step-by-Step

### 1. Prerequisites

- **Node.js** 16 or higher
- **npm** (comes with Node.js)
- **TypeScript** (installed via npm)
- **vsce** (Visual Studio Code Extension Manager)

Install Node.js:
- **Windows**: Download from https://nodejs.org/
- **macOS**: `brew install node`
- **Linux**: `sudo apt install nodejs npm`

Verify:
```bash
node --version  # Should be v16+
npm --version
```

### 2. Install Dependencies

The language server has three package.json files (root, client, server):

```bash
cd tools/taml-language-server

# Install root dependencies
npm install

# This automatically runs postinstall script which:
# - Installs client dependencies
# - Installs server dependencies
```

Or manually:
```bash
# Root
npm install

# Client
cd client && npm install && cd ..

# Server
cd server && npm install && cd ..
```

### 3. Compile TypeScript

```bash
npm run compile
```

This compiles:
- `client/src/extension.ts` → `client/out/extension.js`
- `server/src/server.ts` → `server/out/server.js`

### 4. Package Extension

```bash
# Install vsce if not already installed
npm install -g vsce

# Package
npm run package
```

Creates: `taml-language-server-0.1.0.vsix`

### 5. Install in VSCode

```bash
code --install-extension taml-language-server-0.1.0.vsix
```

Or via GUI:
1. Open VSCode
2. Extensions view (`Ctrl+Shift+X`)
3. `...` menu → "Install from VSIX..."
4. Select `taml-language-server-0.1.0.vsix`

## Development Mode

For active development with hot-reload:

### 1. Open in VSCode

```bash
cd tools/taml-language-server
code .
```

### 2. Start Watch Mode

```bash
npm run watch
```

This watches for changes and recompiles automatically.

### 3. Launch Extension Development Host

- Press `F5` in VSCode
- This opens a new window with the extension loaded
- Open a `.taml` file
- See real-time validation!

### 4. Make Changes

- Edit `server/src/server.ts` for validation logic
- Edit `client/src/extension.ts` for client behavior
- Watch mode automatically recompiles

### 5. Reload Extension

In the Extension Development Host window:
- Press `Ctrl+Shift+P`
- Type "Reload Window"
- Press Enter

Changes take effect immediately!

## Project Structure

```
taml-language-server/
├── package.json              # Root package (extension manifest)
├── tsconfig.json             # Root TypeScript config
├── client/
│   ├── package.json          # Client dependencies
│   ├── tsconfig.json         # Client TypeScript config
│   └── src/
│       └── extension.ts      # VSCode extension entry point
├── server/
│   ├── package.json          # Server dependencies
│   ├── tsconfig.json         # Server TypeScript config
│   └── src/
│       └── server.ts         # Language server implementation
├── .vscodeignore             # Files to exclude from package
├── .gitignore                # Git ignore rules
├── README.md                 # Documentation
└── BUILD.md                  # This file
```

## Build Outputs

After compilation:

```
client/out/
└── extension.js              # Compiled client code

server/out/
└── server.js                 # Compiled server code
```

## Troubleshooting

### npm install fails

**Problem:** Dependencies won't install

**Solutions:**
```bash
# Clear npm cache
npm cache clean --force

# Delete node_modules and reinstall
rm -rf node_modules client/node_modules server/node_modules
npm install
```

### TypeScript compilation errors

**Problem:** `npm run compile` fails

**Solutions:**
```bash
# Check TypeScript version
npx tsc --version

# Reinstall TypeScript
npm install typescript@latest --save-dev

# Compile with verbose output
npx tsc -b --verbose
```

### Extension not activating

**Problem:** Extension doesn't load in VSCode

**Solutions:**
1. Check `package.json` has correct `main` field
2. Verify compiled files exist: `client/out/extension.js`
3. Check VSCode developer console (`Help` → `Toggle Developer Tools`)
4. Reload window

### Server not starting

**Problem:** No diagnostics appearing

**Solutions:**
1. Check Output panel → "TAML Language Server"
2. Enable tracing in settings: `"taml.trace.server": "verbose"`
3. Verify `server/out/server.js` exists
4. Check for errors in developer console

### Wrong file paths

**Problem:** Module not found errors

**Solutions:**
1. Check `client/src/extension.ts` path to server:
   ```typescript
   const serverModule = context.asAbsolutePath(
       path.join('server', 'out', 'server.js')
   );
   ```
2. Ensure paths use forward slashes
3. Verify files exist at expected locations

## Testing

### Manual Testing

1. Build extension
2. Install in VSCode
3. Create test file: `test.taml`
4. Add invalid TAML:
   ```taml
   server
       host	localhost    # Space indentation (error!)
   ```
5. Check for red squiggles

### Test Cases

Create these test files in a workspace:

**valid.taml:**
```taml
server
	host	localhost
	port	8080
```

**invalid-spaces.taml:**
```taml
server
    host	localhost    # Should show error
```

**invalid-tabs-in-value.taml:**
```taml
message	Hello	World    # Should show error
```

## Publishing (Optional)

To publish to VSCode Marketplace:

### 1. Create Publisher

```bash
vsce create-publisher your-name
```

### 2. Get Personal Access Token

- Go to https://dev.azure.com
- User Settings → Personal Access Tokens
- Create token with **Marketplace (Publish)** scope

### 3. Login

```bash
vsce login your-name
```

### 4. Publish

```bash
vsce publish
```

Or increment version:
```bash
vsce publish minor  # 0.1.0 → 0.2.0
vsce publish patch  # 0.1.0 → 0.1.1
vsce publish major  # 0.1.0 → 1.0.0
```

## Continuous Integration

Example GitHub Actions workflow:

```.github/workflows/build.yml
name: Build

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3
      - uses: actions/setup-node@v3
        with:
          node-version: '18'
      - run: npm install
      - run: npm run compile
      - run: npm run package
```

## Resources

- [LSP Specification](https://microsoft.github.io/language-server-protocol/)
- [VSCode Extension API](https://code.visualstudio.com/api)
- [vscode-languageserver](https://www.npmjs.com/package/vscode-languageserver)
- [vscode-languageclient](https://www.npmjs.com/package/vscode-languageclient)

---

**Happy building!** 🚀
