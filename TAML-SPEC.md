# TAML Specification v0.1
## Tab Accessible Markup Language

### Overview
TAML is a minimalist hierarchical data serialization format that uses only tabs and newlines for structure. It's designed to be simpler and more accessible than YAML while maintaining human readability.

### Design Philosophy
- **Minimal markup**: Only tabs and newlines
- **Tab-based hierarchy**: Tabs define both structure and key-value separation
- **No special characters**: No brackets, braces, colons, quotes, or hyphens required
- **Visual clarity**: Structure is immediately visible

### Basic Syntax

#### Key-Value Pairs
Key and value separated by one or more tabs:
```
key	value
key		value
key			value
```
All three examples above are equivalent. Multiple tabs can be used for visual alignment.

#### Nested Structures
Children are indented with tabs. If a key has children, it has no value on the same line:
```
parent
	child	value
	another_child	value
	nested
		deeper	value
```

#### Lists
List items are just values indented one tab from their parent:
```
items
	first item
	second item
	third item
```

#### Nested Lists and Objects
```
config
	database
		host	localhost
		port	5432
		credentials
			username	admin
			password	secret
	features
		authentication
		logging
		caching
```

### Rules

1. **Indentation**: One tab character = one level of nesting
2. **Key-Value Separator**: One or more tab characters between key and value. Multiple tabs can be used for visual alignment.
3. **Line Breaks**: Each key-value pair on its own line
4. **Parent Keys**: Keys with children have no value (just the key alone on the line)
5. **Lists**: Just values indented one tab from their parent key (no special syntax)
6. **No Quotes**: Values are literal strings (no escaping needed)
7. **No Tabs in Content**: Keys and values cannot contain tab characters. Only the separator between key and value may contain tabs.
8. **Comments**: Lines starting with `#` are ignored

### Data Types

TAML is intentionally simple. All values are strings by default. Parsers may interpret:
- Numbers: `42`, `3.14`
- Booleans: `true`, `false`
- Null: `null`

### Example Document

```taml
# TAML Example
application	MyApp
version	1.0.0
author	Developer Name

server
	host	0.0.0.0
	port	8080
	ssl	true
	
database
	type	postgresql
	connection
		host	db.example.com
		port	5432
		database	myapp_db
		
features
	user-authentication
	api-gateway
	rate-limiting
	logging

environments
	development
		debug	true
		log_level	verbose
	production
		debug	false
		log_level	error
```

### Advantages Over YAML

- **Simpler**: No complex syntax for anchors, aliases, or multi-line strings
- **Tab-only**: Tabs do all the work - hierarchy and separation
- **No colons**: One less character to type
- **No ambiguity**: No complex rules about when quotes are needed
- **Easier to write**: Less cognitive load on markup syntax

### Limitations

- Less expressive than YAML (by design)
- Tab characters can be invisible in some editors
- No built-in support for complex data structures or references

### File Extension

`.taml`

### MIME Type

`text/x-taml` or `application/x-taml`

---

*TAML: Because sometimes less markup is more.*
