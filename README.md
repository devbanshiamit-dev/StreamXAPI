# 🎬 StreamXAPI

A production-ready **Movie Management Backend API** built with **ASP.NET Core Web API**.

StreamXAPI provides a scalable backend for managing movies, actors, genres, and their relationships while following a clean layered architecture. The project demonstrates modern backend development practices including **Docker containerization**, **PostgreSQL**, **Entity Framework Core**, **Rate Limiting**, **Global Exception Handling**, and **Cloud Deployment on Railway**.

> ⚠️ This project was built as a learning and portfolio project to strengthen backend engineering skills while exploring real-world deployment workflows.

---

# 🌐 Live API

**Production URL**

```
https://streamxapi-production.up.railway.app
```

---

# 🚀 Features

## 🎥 Movie Management

* Create, Read, Update and Delete Movies
* Search movies by title
* Pagination
* Sorting
* Filtering

## 🎭 Actor Management

* Create, update and delete actors
* Assign actors to movies
* Store character names

## 🎞️ Genre Management

* Create, update and delete genres
* Assign multiple genres to movies

## 🔗 Many-to-Many Relationships

```
Movie
 │
 ├── MovieGenre ─── Genre
 │
 └── MovieActor ─── Actor
```

---

# 🛠 Tech Stack

* ASP.NET Core Web API (.NET 10)
* C#
* Entity Framework Core
* PostgreSQL
* Docker
* Railway
* LINQ
* Repository Pattern
* Dependency Injection
* ASP.NET Core Rate Limiting
* Global Exception Handling
* OpenAPI

---

# 🏗 Architecture

```
Controller
      │
      ▼
Service
      │
      ▼
Repository
      │
      ▼
Entity Framework Core
      │
      ▼
PostgreSQL
```

### Controller Layer

Responsible for:

* HTTP Requests
* Validation
* HTTP Responses

### Service Layer

Responsible for:

* Business Logic
* Data Processing
* Validation Rules

### Repository Layer

Responsible for:

* Database Communication
* Entity Framework Core Queries

---

# 📦 Docker

The application is fully containerized using a multi-stage Docker build.

Docker was used to:

* Build the application
* Publish optimized production binaries
* Run the API inside a container
* Create an environment independent deployment

---

# ☁️ Cloud Deployment

The API is deployed on **Railway**.

Deployment includes:

* Docker-based deployment
* Railway PostgreSQL
* Environment Variables
* Automatic deployment from GitHub

---

# 🗄 Database

Originally developed using SQL Server and later migrated to PostgreSQL for cloud deployment.

Database includes:

* Movies
* Actors
* Genres
* MovieActor
* MovieGenre

Entity Framework Core Migrations are used for database version control.

---

# 📌 API Features

## Pagination

```
GET /api/movie?pageNumber=1&pageSize=10
```

Supports:

* Page Number
* Page Size
* Total Records
* Total Pages

---

## Search

```
GET /api/movie?search=Avengers
```

---

## Sorting

```
GET /api/movie?sortBy=rating&sortOrder=desc
```

---

## Filtering

Example:

```
GET /api/movie?year=2024
```

---

# 🔐 Rate Limiting

ASP.NET Core built-in Rate Limiting middleware is implemented.

Current Configuration:

```
30 Requests / Minute
```

When exceeded:

```
HTTP 429
Too Many Requests
```

Purpose:

* Prevent API abuse
* Reduce unnecessary server load
* Protect backend resources

---

# ⚠️ Global Exception Handling

Centralized exception handling provides consistent API responses.

Example:

```json
{
  "title": "Internal Server Error",
  "status": 500,
  "detail": "Something went wrong"
}
```

Benefits:

* Cleaner controllers
* Consistent responses
* Better debugging
* No sensitive information leakage

---

# 📂 Project Structure

```
StreamXAPI

├── Controllers
├── Services
├── Repository
├── Models
├── DTOs
├── Data
├── Middleware
├── Migrations
├── Dockerfile
└── Program.cs
```

---

# ⚙️ Running Locally

Clone the repository

```bash
git clone https://github.com/devbanshiamit-dev/StreamXAPI.git
```

Navigate to the project

```bash
cd StreamXAPI
```

Configure the database connection using the following Environment Variable:

```
ConnectionStrings__DefaultConnection
```

Run the project

```bash
dotnet run
```

---

# 🐳 Running with Docker

Build the image

```bash
docker build -t streamxapi .
```

Run the container

```bash
docker run -p 8080:8080 \
-e ConnectionStrings__DefaultConnection="YOUR_CONNECTION_STRING" \
streamxapi
```

---

# 🔒 Configuration

This project uses **Environment Variables** for sensitive configuration such as database connection strings.

Example:

```
ConnectionStrings__DefaultConnection
```

Secrets are intentionally **not stored** inside the repository.

---

# 🚀 Future Improvements

* JWT Authentication & Authorization
* Refresh Token System
* Role-Based Authorization
* Logging
* Unit Testing
* Redis Caching
* Health Checks
* CI/CD Pipeline
* React Frontend Integration
* Kubernetes Deployment

---

# 👨‍💻 About This Project

This project was built as part of my backend engineering learning journey.

The primary objective was not only to develop a REST API, but also to gain hands-on experience with:

* Docker
* PostgreSQL
* Entity Framework Core
* Cloud Deployment
* Production-style configuration using Environment Variables

---

# 📜 License

This project is intended for learning, portfolio, and demonstration purposes.
