# TfL Backend Service

Backend service built with **ASP.NET Core (.NET)** that consumes and normalizes data from the **Transport for London (TfL) Open Data API**.
The goal of this project is to demonstrate **backend, infrastructure and systems-oriented development**, focusing on reliability, abstraction and production-like concerns rather than UI or visualization.

---

## Project Goals

This project simulates an **internal backend service** that:

* Consumes data from an **external, unreliable dependency** (TfL API)
* Normalizes and exposes **stable internal endpoints**
* Applies backend best practices such as:

  * Dependency Injection
  * Separation of concerns
  * Resilience and fault tolerance (timeouts, retries, caching)
  * Observability (health checks, logging)

There is **no frontend** by design. All outputs are raw JSON intended to be consumed by other systems.

---

## Architecture Overview

High-level structure:

```
Clients/        → External API clients (TfL)
Controllers/    → HTTP endpoints (thin controllers)
Services/       → Domain and business logic
Models/         → Internal normalized models
Infrastructure/ → Cross-cutting concerns (caching, config, etc.)
```

Design principles:

* Controllers contain **no business logic**
* External APIs are accessed **only through clients**
* Internal models are **decoupled** from external DTOs
* Failures are handled gracefully whenever possible

---

## External Dependency

### Transport for London (TfL) Open Data API

* Provider: Transport for London
* Documentation: [https://api.tfl.gov.uk](https://api.tfl.gov.uk)
* Data type: Transport lines, stations, statuses and incidents

The TfL API is treated as:

* Rate-limited
* Potentially unstable
* Subject to change

This backend acts as a **protective layer** between TfL and downstream consumers.

---

## Running the Project Locally

### Prerequisites

* .NET SDK (8 LTS or newer)
* Git
* VS Code (or any editor)

### Steps

```bash
git clone https://github.com/xabierfj/tfl-backend-service.git
cd tfl-backend-service
dotnet run
```

The API will start listening on a local port (shown in the console output).

---

## Health Check Endpoint

Basic service health endpoint:

```
GET /health
```

Example response:

```json
{
  "status": "UP",
  "timestamp": "2026-01-18T12:00:00Z"
}
```

This endpoint is intended for monitoring and orchestration systems.

---

##  Planned Endpoints

Planned internal endpoints (subject to evolution):

```
GET /lines
GET /lines/{id}/stations
GET /incidents
GET /network/graph
```

These endpoints will:

* Normalize TfL data
* Apply caching strategies
* Remain stable even if the upstream API changes

---

## Configuration

TfL configuration is defined via `appsettings.json`:

```json
{
  "TfL": {
    "BaseUrl": "https://api.tfl.gov.uk",
    "AppId": "",
    "AppKey": ""
  }
}
```

API keys are optional for development but recommended to avoid rate limits.

## License

This project is for educational and portfolio purposes.
