# TAML Tools

This directory contains tools and utilities for working with TAML (Tab Accessible Markup Language).

## Available Tools

### 🎨 [vscode-taml](./vscode-taml/) - Visual Studio Code Syntax Highlighter

Syntax highlighting and language support for TAML files in VSCode.

**Features:**
- Syntax highlighting for `.taml` files
- Comment support
- Indentation-based folding
- Auto-configuration for tab-based editing
- Special value highlighting (booleans, numbers, null)
- Environment variable highlighting

**Quick Start:**
```bash
cd vscode-taml
npm install -g vsce
vsce package
code --install-extension taml-*.vsix
```

See [vscode-taml/INSTALL.md](./vscode-taml/INSTALL.md) for detailed instructions.

---

### 🔴 [taml-language-server](./taml-language-server/) - Language Server Protocol (LSP)

**Real-time validation with red squiggles!** A complete LSP implementation for TAML.

**Features:**
- ✅ Real-time error detection as you type
- 🔴 Red squiggles for errors
- ⚠️ Yellow squiggles for warnings
- 📋 Problems panel integration
- ⚙️ Configurable validation settings
- 🎯 Matches C# validator exactly

**Validates:**
- Space indentation (must use tabs)
- Mixed tabs and spaces
- Inconsistent indentation levels
- Orphaned indentation
- Tabs in values
- Empty keys
- Invalid structure

**Quick Start:**
```bash
cd taml-language-server
npm install
npm run compile
vsce package
code --install-extension taml-language-server-*.vsix
```

See [taml-language-server/QUICKSTART.md](./taml-language-server/QUICKSTART.md) for setup.

**Recommended Setup:**
Install **both** extensions for the complete experience:
1. **vscode-taml** → Beautiful syntax highlighting
2. **taml-language-server** → Real-time error detection

## Future Tools

### 🔧 Planned Tools

#### taml-cli
Command-line tool for TAML file manipulation:
- Convert TAML ↔ JSON
- Convert TAML ↔ YAML
- Validate TAML files
- Format/prettify TAML
- Merge TAML files

#### taml-lint
Linter for TAML files:
- Check syntax validity
- Enforce style conventions
- Detect common mistakes
- Suggest improvements

#### taml-fmt
Formatter for TAML files:
- Align values automatically
- Normalize indentation
- Sort keys alphabetically (optional)
- Strip trailing whitespace

#### taml-diff
Specialized diff tool for TAML:
- Semantic diff (ignores tab count differences)
- Hierarchical diff visualization
- Merge conflict resolver

#### Language Server Protocol (LSP)
Full-featured language server for TAML:
- Auto-completion
- Go to definition
- Find references
- Hover documentation
- Real-time validation
- Rename refactoring

#### Sublime Text Package
Syntax highlighting for Sublime Text

#### Atom Package
Syntax highlighting for Atom

#### Vim/Neovim Plugin
Syntax highlighting and indentation for Vim

#### IntelliJ Plugin
Support for JetBrains IDEs

#### Online Playground
Web-based TAML editor:
- Live syntax highlighting
- Convert between formats
- Share TAML snippets
- Example gallery

## Contributing

Want to build a tool for TAML? Great!

1. Create a new directory in `tools/`
2. Add documentation in README.md
3. Follow TAML specification in [../TAML-SPEC.md](../TAML-SPEC.md)
4. Test with examples in [../examples/](../examples/)
5. Submit a pull request

### Tool Guidelines

- **Simple**: Keep tools focused and easy to use
- **Cross-platform**: Support Windows, macOS, Linux
- **Well-documented**: Include README, examples, and installation instructions
- **Consistent**: Follow TAML specification exactly
- **Accessible**: Consider accessibility in tool design

## Resources

- [TAML Specification](../TAML-SPEC.md)
- [TAML Examples](../examples/)
- [.NET Serializer](../dotnet/)

## License

All tools in this directory inherit the license from the main TAML project (see [../LICENSE](../LICENSE)).

---

**Build tools that make TAML accessible and useful for everyone!** 🚀
