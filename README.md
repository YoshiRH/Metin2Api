# Metin2 API 🎮

An educational project inspired by the classic MMORPG **Metin2**.  
The goal of this project is to simulate **account, character, inventory, item, and guild management** in a game-like environment, following clean architecture principles.

---

## ✨ Features

- **Accounts**
  - Create and manage game accounts.
  - Each account can store up to **4 characters**.
- **Characters**
  - Each character has:
    - Nickname
    - Level
    - Kingdom (Jinno, Shinsoo, Chunjo)
    - Class (Warrior, Ninja, Sura, Shaman)
    - Inventory
- **Inventory & Items**
  - Each character has its own inventory.
  - Add and manage multiple items.
- **Guilds (new!)**
  - Create guilds.
  - Add/remove characters from guilds.
  - Retrieve all characters within a guild.
- **CRUD API Endpoints** for all entities with validation rules (e.g., max 4 characters per account).

---

## 🛠️ Tech Stack

- **C# / ASP.NET Core 9**
- **Entity Framework Core** (Code-First approach)
- **PostgreSQL** as database
- **Clean Architecture** with layered design:
  - `Domain` → Entities & Interfaces
  - `Application` → Business logic, DTOs, Services
  - `Infrastructure` → Database (EF Core), Repositories
  - `API` → Controllers & Endpoints
- **Scalar** → API documentation
- Dependency Injection for services and repositories

---

## 📂 Project Structure
Metin2Api/  
├── Metin2Api/ → ASP.NET Core Web API (Startup project)  
│ └── Program.cs  
├── Metin2Api.Domain/ → Entities, Enums, Repository interfaces  
├── Metin2Api.Application/ → DTOs, Services, Service interfaces  
├── Metin2Api.Infrastructure/ → EF Core DbContext, Repositories, Migrations  

---

## 📖 API Endpoints (Examples)
**Accounts**
- POST /api/accounts → Create new account
- GET /api/accounts → Get all accounts
- DELETE /api/accounts/{id} → Delete account

**Characters**
- POST /api/accounts/{accountId}/characters → Add character to account
- GET /api/characters → Get all characters
- GET /api/accounts/{accountId}/characters → Get all characters of an account

**Items**
- POST /api/characters/{characterId}/items → Add item to character inventory
- GET /api/items → Get all items
- GET /api/items/{id} → Get item by id

**Guilds**
- POST /api/guilds → Create new guild
- POST /api/guilds/{guildId}/characters/{characterId} → Add character to guild
- GET /api/guilds/{guildId}/characters → Get all characters in a guild
