# 🎬 StreamXAPI

StreamXAPI is a **Movie Management Backend API** built using **ASP.NET Core Web API**.
It provides a structured and scalable backend system to manage movies, actors, genres, and their relationships.

The API follows a clean architecture approach with separation between controllers, services, repositories, and database layers.

---

# 🚀 Features

## 🎥 Movie Management

* Create, Read, Update, Delete Movies
* Search movies by title
* Pagination support
* Sorting support
* Filtering support

## 🎭 Actor Management

* Create, update, delete actors
* Manage actor details
* Assign actors to movies
* Store character names

## 🎞️ Genre Management

* Create, update, delete genres
* Assign multiple genres to movies

## 🔗 Movie Relationships

Implemented many-to-many relationships:

```
Movie
 |
 |--- MovieGenre --- Genre


Movie
 |
 |--- MovieActor --- Actor
```

---

# 🛠️ Tech Stack

* ASP.NET Core Web API
* C#
* Entity Framework Core
* SQL Server
* LINQ
* Swagger / OpenAPI
* Repository Pattern
* Dependency Injection

---

# 🏗️ Architecture

StreamXAPI follows a layered architecture:

```
Controller Layer
        |
        ↓
Service Layer
        |
        ↓
Repository Layer
        |
        ↓
Entity Framework Core
        |
        ↓
SQL Server Database
```

### Controller Layer

Handles:

* HTTP requests
* Request validation
* HTTP responses

### Service Layer

Handles:

* Business logic
* Data processing
* Validation rules

### Repository Layer

Handles:

* Database communication
* Entity Framework Core queries

---

# 📌 API Features

## Pagination

Example:

```
GET /api/movie?pageNumber=1&pageSize=10
```

Supports:

* Page number
* Page size
* Total records
* Total pages

---

## Search

Example:

```
GET /api/movie?search=Avengers
```

Search movies by title.

---

## Sorting

Example:

```
GET /api/movie?sortBy=rating&sortOrder=desc
```

Supports sorting based on movie fields.

---

## Filtering

Movies can be filtered using available query parameters.

Example:

```
GET /api/movie?year=2024
```

---

# 🔐 Rate Limiting

StreamXAPI implements ASP.NET Core built-in Rate Limiting middleware.

Purpose:

* Prevent API abuse
* Protect server resources
* Control excessive requests

Current configuration:

```
Limit: 30 requests per minute
Response: 429 Too Many Requests
```

When the limit is exceeded:

```
HTTP 429 - Too Many Requests
```

is returned.

---

# ⚠️ Exception Handling

Implemented global exception handling middleware.

Benefits:

* Centralized error management
* Consistent API error responses
* Prevents exposing sensitive server details

Example response:

```json
{
  "title": "Internal Server Error",
  "status": 500,
  "detail": "Something went wrong"
}
```

---

# 🗄️ Database Design

Main entities:

```
Movie
 |
 |--- MovieActor
 |          |
 |          Actor


Movie
 |
 |--- MovieGenre
            |
            Genre
```

---

# 📂 Project Structure

```
StreamXAPI

├── Controllers
│
├── Services
│
├── Repository
│
├── Models
│
├── DTO
│
├── Data
│
├── Middleware
│
└── Program.cs
```

---

# 📖 API Documentation

Swagger UI is available for API testing and documentation.

Example:

```
https://localhost:7159/swagger
```

---

# 🔮 Future Improvements

Planned features:

* JWT Authentication & Authorization
* Refresh Token System
* Multi-language Movie Support
* Response DTO Improvements
* Unit Testing
* Logging System
* React Frontend Integration
* Cloud Deployment

---

# 👨‍💻 Developer

Built with ❤️ using ASP.NET Core Web API.

---

# 📜 License

This project is created for learning, portfolio, and demonstration purposes.
