# StreamXAPI

A RESTful movie catalog API built with **ASP.NET Core (.NET 10)** and **PostgreSQL**, featuring clean layered architecture, rate limiting, global exception handling, and full CRUD across Movies, Genres, and Actors — including many-to-many relationship management between them.

**🔗 Live API:** [streamxapi-production.up.railway.app](https://streamxapi-production.up.railway.app)

---

## Features

- **Movie management** — full CRUD with pagination and query filtering
- **Genre & Actor management** — full CRUD
- **Movie relationships** — attach/detach genres and actors to a movie
- **Rate limiting** — fixed-window limiter (30 requests/min) on movie endpoints
- **Global exception handling** — custom exceptions mapped to RFC 7807 `ProblemDetails` responses
- **Repository pattern** — clean separation between controllers, services, and data access
- **PostgreSQL + EF Core** — via Npgsql provider
- **Dockerized** — multi-stage build, deployed on Railway

---

## Tech Stack

| Layer          | Technology                          |
|----------------|--------------------------------------|
| Framework      | ASP.NET Core Web API (.NET 10)      |
| Database       | PostgreSQL                          |
| ORM            | Entity Framework Core (Npgsql)      |
| Architecture   | Controller → Service → Repository   |
| Rate Limiting  | `Microsoft.AspNetCore.RateLimiting` |
| Error Handling | Custom `IExceptionHandler` + `ProblemDetails` |
| Containerization | Docker (multi-stage build)        |
| Hosting        | Railway                             |

---

## Architecture

The project follows a clean, layered structure to keep concerns separated and testable:

```
Controllers   → HTTP endpoints, request/response handling
Services      → Business logic
Repo          → Data access (interfaces + implementations)
DTO           → Request/response contracts (per entity)
Models        → EF Core entities
Pagination    → Query parameter models for filtering/paging
MiddleWare    → Global exception handling
Data          → AppDbContext (EF Core)
```

Each entity (Movie, Genre, Actor) has its own repository interface/implementation and service interface/implementation, injected via built-in DI (`AddScoped`).

---

## API Endpoints

### Movies — `/api/movie`

| Method | Endpoint                              | Description                          |
|--------|----------------------------------------|---------------------------------------|
| GET    | `/api/movie`                           | Get all movies (supports pagination/filtering via query params) |
| GET    | `/api/movie/{id}`                      | Get a movie by ID                    |
| POST   | `/api/movie`                           | Create a new movie                   |
| PUT    | `/api/movie/{id}`                      | Update a movie                       |
| DELETE | `/api/movie/{id}`                      | Delete a movie                       |
| POST   | `/api/movie/{movieId}/genres`          | Attach genre(s) to a movie           |
| DELETE | `/api/movie/{movieId}/genres/{genreId}`| Remove a genre from a movie          |
| POST   | `/api/movie/{movieId}/actors`          | Attach actor(s) to a movie           |
| DELETE | `/api/movie/{movieId}/actors/{actorId}`| Remove an actor from a movie         |

> ⚠️ `POST /api/movie/seed` exists for bulk-seeding test data during development. Recommended to remove or guard this endpoint before a production-hardening pass.

All movie endpoints are rate-limited to **30 requests per minute per client** (fixed-window, no queueing — see [Rate Limiting](#rate-limiting)).

### Genres — `/api/genre`

| Method | Endpoint             | Description          |
|--------|-----------------------|-----------------------|
| GET    | `/api/genre`          | Get all genres       |
| GET    | `/api/genre/{id}`     | Get a genre by ID    |
| POST   | `/api/genre`          | Create a new genre   |
| PUT    | `/api/genre/{id}`     | Update a genre       |
| DELETE | `/api/genre/{id}`     | Delete a genre       |

### Actors — `/api/actor`

| Method | Endpoint             | Description          |
|--------|-----------------------|-----------------------|
| GET    | `/api/actor`          | Get all actors       |
| GET    | `/api/actor/{id}`     | Get an actor by ID   |
| POST   | `/api/actor`          | Create a new actor   |
| PUT    | `/api/actor/{id}`     | Update an actor      |
| DELETE | `/api/actor/{id}`     | Delete an actor      |

---

## Rate Limiting

The `GET /api/movie` and other movie endpoints are protected with a **fixed-window rate limiter**:

- **Window:** 1 minute
- **Limit:** 30 requests
- **Queue:** none (excess requests are rejected immediately)
- **Rejection status:** `429 Too Many Requests`

Configured in `Program.cs` via `AddRateLimiter` and applied with `[EnableRateLimiting("fixed")]` on the `MovieController`.

---

## Error Handling

All exceptions are funneled through a centralized `GlobalExceptionHandler` (implementing `IExceptionHandler`) and returned as standardized `ProblemDetails` responses, keeping error shapes consistent across the API instead of leaking raw stack traces or ad-hoc error formats.

---

## Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- PostgreSQL instance (local or hosted)

### Setup

1. **Clone the repo**
   ```bash
   git clone https://github.com/devbanshiamit-dev/StreamXAPI.git
   cd StreamXAPI
   ```

2. **Configure the connection string**

   In `StreamXAPI/appsettings.json` (or `appsettings.Development.json`):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=streamxdb;Username=postgres;Password=yourpassword"
     }
   }
   ```

3. **Apply EF Core migrations**
   ```bash
   dotnet ef database update --project StreamXAPI
   ```

4. **Run the API**
   ```bash
   dotnet run --project StreamXAPI
   ```

5. Open the OpenAPI/Swagger doc (available in Development mode) at `/openapi` (or your configured route) to explore endpoints interactively.

---

## Running with Docker

The repo includes a multi-stage `Dockerfile` (SDK build → ASP.NET runtime image).

```bash
# Build the image
docker build -t streamxapi .

# Run the container
docker run -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="Host=your-db-host;Port=5432;Database=streamxdb;Username=postgres;Password=yourpassword" \
  streamxapi
```

The container listens on port `8080`.

---

## Deployment (Railway)

This project is deployed on [Railway](https://railway.app) directly from the Dockerfile.

**Live URL:** [https://streamxapi-production.up.railway.app](https://streamxapi-production.up.railway.app)

To deploy your own instance:

1. Push the repo to GitHub.
2. In Railway, create a **New Project → Deploy from GitHub repo** and select `StreamXAPI`.
3. Railway detects the `Dockerfile` automatically and builds the image.
4. Add a PostgreSQL plugin (or connect an external instance) and set the environment variable below.
5. Deploy — Railway assigns a public `*.up.railway.app` domain.

### Environment Variables

| Variable                              | Description                          |
|-----------------------------------------|---------------------------------------|
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string (double underscore for nested config in env vars) |

---

## Project Structure

```
StreamXAPI/
├── Controllers/        # MovieController, GenreController, ActorController
├── Services/            # Business logic (IMovieService, IGenresService, IActorService)
├── Repo/                # Data access (IMovieRepository, IGenreRepository, IActorRepository)
├── DTO/                 # Request/response DTOs per entity
├── Models/               # EF Core entities
├── Data/                 # AppDbContext
├── MiddleWare/            # GlobalExceptionHandler
├── Pagination/            # MovieQueryParams and related filtering models
├── Program.cs
└── Dockerfile
```

---

## Roadmap / Ideas

- [ ] Add authentication/authorization (JWT) for write endpoints
- [ ] Remove or gate the `/api/movie/seed` test endpoint
- [ ] Add automated tests (unit + integration)
- [ ] Add response caching for read-heavy endpoints
- [ ] Extend rate limiting to Genre/Actor endpoints

---

## Author
Amit Devbasnhi 🫩
**Amit Devbanshi (Robin)**
Backend Developer — ASP.NET Core / C#
[GitHub](https://github.com/devbanshiamit-dev)
