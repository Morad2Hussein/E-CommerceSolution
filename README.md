# 🛒 E-Commerce Web API
**High-Performance Backend | Onion Architecture | Redis Caching | JWT Security**



A production-ready RESTful API built for modern e-commerce platforms. This project emphasizes **Architectural Purity** using Onion Architecture and focuses on high-speed data retrieval through distributed caching.

---

## 🏛 Architectural Excellence: Onion Layering

The system is engineered to be highly testable and independent of external frameworks:
* **Core Layer:** Contains Domain Entities and Interfaces (The heart of the system).
* **Application Layer:** Handles Business Logic, DTOs, and Mapping logic.
* **Infrastructure Layer:** Manages external concerns like **SQL Server** and **Redis Caching**.
* **Web API Layer:** The entry point, kept "thin" and clean using **Extensions Configuration** for service registrations.

### 🧩 Advanced Patterns & Logic
* **Distributed Caching:** Integrated **Redis** (via StackExchange.Redis) to drastically reduce latency for frequently accessed products and categories.
* **Unit of Work & Repository:** Ensures that complex checkout or inventory operations are handled within a single, secure transaction.
* **Stateless Security:** Implemented **JWT Bearer Authentication** for secure, scalable user sessions.

---

## 🛠️ Tech Stack & Performance Tools

| Category | Technology |
| :--- | :--- |
| **Framework** | ASP.NET Core Web API |
| **Primary Database** | Microsoft SQL Server |
| **Caching Layer** | Redis (High-Performance NoSQL) |
| **Security** | JWT (JSON Web Tokens) |
| **Documentation** | Swagger / Swashbuckle |
| **Mapping** | AutoMapper |

---

## 🚀 Key Features

- ⚡ **Redis Integration:** Optimized read performance for product catalogs.
- 🔐 **Secure Access:** Identity management and protected endpoints using JWT.
- 🛠 **Clean Startup:** Used **Service Extensions** to keep `Program.cs` readable and modular.
- 📂 **AutoMapper Implementation:** Clean transformation between Domain Entities and API Response DTOs.
- 📑 **Interactive API Docs:** Fully documented endpoints with Swagger for easy frontend integration.
- 🔄 **Transaction Integrity:** Unit of Work pattern ensuring database consistency during orders.

---

## 📁 Project Structure (Onion Model)

```text
ECommerce.API/
├── 📂 Core.Domain           # Entities, Aggregates, Constants
├── 📂 Core.Application      # Interfaces, Services, DTOs, Mapping Profiles
├── 📂 Infrastructure.Data   # DbContext, Migrations, Repositories, Redis Implementation
└── 📂 Web.API               # Controllers, Middlewares, Program Extensions
