# TAML Examples

This directory contains real-world examples demonstrating TAML's readability and usability across different domains.

## Examples

### 1. [**web-app-config.taml**](./web-app-config.taml) - Web Application Configuration
Modern web app config with nested settings for server, database, auth, and monitoring. Shows how TAML makes complex configs readable.

**Best for:** DevOps, Backend Engineers, SREs

**Highlights:**
- Multi-level nesting for logical grouping
- Environment variable placeholders
- Clean separation of concerns
- Tab-aligned for easy scanning

**Sample:**
```taml
server
	host			0.0.0.0
	port			3000
	workers			4
	ssl
		enabled			true
		cert			/etc/ssl/certs/app.crt
		protocols
			TLSv1.2
			TLSv1.3

database
	primary
		type			postgresql
		host			db-primary.example.com
		pool_size		20
```

### 2. [**api-documentation.taml**](./api-documentation.taml) - REST API Documentation
Complete API endpoint documentation with parameters, responses, and rate limits. Replaces verbose OpenAPI/Swagger for simple APIs.

**Best for:** API Developers, Technical Writers, Frontend Engineers

**Highlights:**
- Self-documenting structure
- Easy to navigate with keyboard
- Query/body/path params clearly separated
- Response codes and descriptions

**Sample:**
```taml
endpoints
	create_user
		method			POST
		path			/users
		auth_required	true
		body_params
			email
				type			string
				required		true
				format			email
			name
				type			string
				required		true
				min_length		2
		response_codes
			201
				description		User created
			400
				description		Invalid input
```

### 3. [**cloud-infrastructure.taml**](./cloud-infrastructure.taml) - Infrastructure as Config
AWS infrastructure definition with compute, storage, networking, and security. Alternative to verbose CloudFormation/Terraform for simple setups.

**Best for:** Cloud Architects, DevOps Engineers, Infrastructure Teams

**Highlights:**
- Hierarchical resource organization
- Clear relationships between components
- Tag management
- Auto-scaling configurations

**Sample:**
```taml
compute
	web_servers
		instance_type		t3.medium
		count				3
		auto_scaling
			min				2
			max				10
			target_cpu		70
		tags
			environment		production
			team			platform

storage
	databases
		postgres_primary
			engine			postgres
			version			15.2
			storage			500
			multi_az		true
```

### 4. [**game-level-design.taml**](./game-level-design.taml) - Game Level Configuration
Complete game level definition with enemies, puzzles, collectibles, and objectives. Perfect for game designers who prefer data-driven design.

**Best for:** Game Designers, Level Designers, Game Developers

**Highlights:**
- Designer-friendly (no programming knowledge needed)
- Spatial coordinates for placement
- Enemy AI and behavior configs
- Loot tables and progression

**Sample:**
```taml
enemies
	temple_guard
		type			warrior
		count			8
		health			500
		damage			35
		ai_behavior		patrol
		loot
			gold_coins
				min			10
				max			25
				chance		80

collectibles
	treasure_chest_1
		x				300
		y				150
		contents
			rare_sword
				level		25
				damage		150
```

### 5. [**recipe-database.taml**](./recipe-database.taml) - Recipe Collection
Detailed recipe with ingredients, instructions, nutrition, and metadata. Great for food bloggers and recipe management systems.

**Best for:** Food Bloggers, Recipe Apps, Cooking Websites

**Highlights:**
- Structured ingredient lists with units
- Step-by-step instructions
- Nutrition information
- Equipment and timing

**Sample:**
```taml
recipe_name			Classic Margherita Pizza
prep_time			30
cook_time			15
servings			4

ingredients
	dough
		all_purpose_flour
			amount			500
			unit			grams
		warm_water
			amount			325
			unit			ml

instructions
	knead
		step			3
		action			Combine wet and dry, knead for 10 minutes
		time			10
		notes			Add flour if too sticky
```

### 6. [**team-directory.taml**](./team-directory.taml) - Team Organization Directory
Engineering team structure with members, responsibilities, tools, and schedules. Perfect for internal wikis and team documentation.

**Best for:** Engineering Managers, HR, Team Leads

**Highlights:**
- Org chart in data format
- Contact information
- Timezone-aware
- On-call rotations

**Sample:**
```taml
teams
	platform
		team_lead
			name			Alex Rivera
			email			alex.r@techinnovations.com
			timezone		America/Chicago
		
		members
			priya_patel
				role			senior_engineer
				timezone		Asia/Kolkata
				specialties
					kubernetes
					terraform
					aws
				on_call		week_1
		
		tools
			slack			#team-platform
			jira			PLAT
			github			platform-team
```

## Why TAML Works for These Use Cases

### ✅ **Accessibility**
- Navigate entire configs with just Tab and Arrow keys
- Screen reader friendly
- No complex syntax to remember

### ✅ **Readability**
- Visual hierarchy matches logical hierarchy
- Aligned values easy to scan
- Minimal noise, maximum clarity

### ✅ **Editability**
- Quick to modify by hand
- Hard to make syntax errors
- Version control friendly

### ✅ **Machine Parseable**
- Simple, unambiguous parsing rules
- Fast to serialize/deserialize
- Language-agnostic

## Try It Yourself

```bash
# Serialize C# objects to TAML
var config = new WebAppConfig { /* ... */ };
var taml = TamlSerializer.Serialize(config);

# Deserialize TAML back to objects
var config = TamlSerializer.Deserialize<WebAppConfig>(taml);
```

## When to Use TAML vs Other Formats

| Format | Best For | TAML Better When... |
|--------|----------|---------------------|
| **JSON** | APIs, data interchange | You need human readability |
| **YAML** | Complex configs | You want simplicity over features |
| **TOML** | Simple configs | You prefer visual hierarchy |
| **XML** | Enterprise systems | You hate angle brackets |
| **INI** | Legacy configs | You need nesting |

## Contributing Examples

Have a great TAML use case? Add it to this directory! Examples should:
- Be realistic and practical
- Demonstrate TAML's strengths
- Include comments explaining the structure
- Use tab alignment for readability
