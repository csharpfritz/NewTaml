# Extension Images

This directory contains images for the TAML VSCode extension.

## Required Images

### icon.png
- Size: 128x128 pixels
- Format: PNG
- Purpose: Extension icon in marketplace and extension manager
- Should be: Simple, recognizable TAML logo

### taml-icon.svg
- Format: SVG
- Purpose: File icon in VSCode file explorer
- Should be: Small icon that represents `.taml` files

### taml-banner.png (optional)
- Size: 1280x640 pixels recommended
- Format: PNG
- Purpose: Banner image in extension details page
- Should be: Professional banner with TAML branding

## Creating Icons

You can create simple placeholder icons using any image editor or online tools like:
- https://www.canva.com
- https://www.figma.com
- https://www.photopea.com

### Suggested Design Elements

**Theme:** Tabs and accessibility
- Use tab symbol (→) or keyboard imagery
- Color scheme: Blue/teal (tech-friendly)
- Simple, minimal design
- High contrast for visibility

**Icon Sketch Ideas:**
1. A simple "T" for TAML with tab lines
2. Tab key symbol with arrows
3. Nested brackets made of tab symbols
4. Document with visible indentation lines

## Temporary Placeholder

For now, the extension will work without custom icons. VSCode will use default file icons until these are added.

To add icons later:
1. Create the image files
2. Place them in this directory
3. Update paths in `package.json` if needed
4. Rebuild the extension: `vsce package`
