# **TAML: A Minimalist, Tab‑Annotated Markup Language for Humans and Machines**

## **Overview**
TAML (Tab Annotated Markup Language) is a radically simple, whitespace‑driven markup format defined exclusively by **tabs** and **newlines**. Designed as a clean, deterministic alternative to YAML and other indentation‑sensitive formats, TAML emphasizes clarity, predictability, and human readability. Its strict grammar eliminates ambiguity while remaining approachable for both technical and non‑technical users.

## **Why TAML Exists**
Modern markup languages often introduce subtle whitespace rules, invisible‑character pitfalls, and inconsistent parsing behavior. YAML in particular is notorious for indentation surprises and hard‑to‑spot formatting errors.

TAML solves these problems by embracing a **minimal, explicit grammar** that is easy to read, easy to write, and easy to parse — without sacrificing expressiveness.

## **Core Principles**
- **Entries are separated by carriage returns** (CR or CRLF).  
- **Keys and values are separated by one or more tab characters** (`\t`).  
- **Hierarchy is defined by leading tabs**, representing nested structures.  
- **Arrays are represented naturally** by repeating keys with child values.  
- **Spaces are illegal**, eliminating ambiguity and invisible‑character bugs.  
- **Comments are supported** using a leading `#` at the start of a line.  
- **Documents may be visually indented** to create clean, readable layouts — ideal for non‑technical audiences.  
- **Recommended MIME type:** `application/taml`.

---

## **Comparison: TAML vs YAML vs JSON**

| Feature / Behavior | **TAML** | **YAML** | **JSON** |
|-------------------|----------|----------|----------|
| **Structure defined by** | Tabs + newlines | Spaces + newlines | Braces + brackets |
| **Whitespace rules** | Strict, simple, explicit | Complex, error‑prone | Mostly irrelevant |
| **Comments** | Yes (`#` at line start) | Yes | No (except via extensions) |
| **Human readability** | Excellent — visually clean, indentation‑first | Good but fragile | Moderate — punctuation‑heavy |
| **Machine parsing** | Very easy, deterministic | Hard — many edge cases | Very easy |
| **Ideal for non‑technical users** | Yes | Sometimes | Rarely |
| **Verbosity** | Low | Low–medium | Medium–high |
| **Ambiguity risk** | None (spaces illegal) | High | None |
| **Learning curve** | Minutes | Hours | Minutes |

---

## **Visual Example: A Simple TAML Document**

Below is a clean, readable TAML example showing hierarchy, arrays, and comments:

```
# Application configuration

app	name	MyTagg
app	version	1.0

app	settings
	app	settings	theme	dark
	app	settings	notifications	enabled

users
	users	user
		user	id	1
		user	name	Fritz

	users	user
		user	id	2
		user	name	Charlie
```

### **What this example demonstrates**
- **Hierarchy** via leading tabs  
- **Key/value separation** via tabs  
- **Arrays** via repeated keys (`users → user`)  
- **Comments** using `#`  
- **Readable indentation** that feels natural to both developers and non‑technical readers  

---

## **Benefits**

### **1. Extreme Simplicity**
TAML’s entire grammar fits on a single page. Developers can learn it in minutes, and tooling authors can implement parsers in hours.

### **2. Deterministic Parsing**
Only tabs and newlines define structure. No optional syntaxes. No hidden rules. No surprises.

### **3. Readable for Everyone**
TAML’s indentation‑first layout mirrors how people naturally structure information. Non‑technical users can understand TAML documents at a glance.

### **4. Ideal for Configuration and Data Transport**
Perfect for configuration files, lightweight data interchange, embedded systems, CLIs, and any scenario where YAML is too loose and JSON is too verbose.

### **5. Cross‑Language Support**
Parsers and handlers already exist across multiple languages, with community contributions growing.

---

## **Future Potential**
With its deterministic structure, whitespace‑only grammar, and approachable visual layout, TAML has the potential to become a universal format for configuration and structured text. Planned enhancements — including richer tooling, editor integrations, and expanded language support — will continue to strengthen its ecosystem.

---

If you want, I can also help you:

- Turn this into a **GitHub README**  
- Create a **landing page** for taml.dev  
- Produce a **conference‑ready slide deck**  
- Draft a **launch announcement** or **intro blog post**

Just tell me where you want to take it next.