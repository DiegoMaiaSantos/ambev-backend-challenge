# Sales API

This project implements a Sales API as part of a developer evaluation.  
It follows Clean Architecture principles using .NET, MediatR, and Entity Framework Core.

---

## Features

- Create Sale
- Get Sale by Id
- List Sales
- Update Sale
- Cancel Sale

---

## Business Rules

- Purchases with 4 or more identical items receive a 10% discount
- Purchases with 10 to 20 identical items receive a 20% discount
- It is not allowed to sell more than 20 identical items
- Purchases with less than 4 items do not receive a discount

---

## Events

The following domain events are simulated using logging:

- SaleCreated
- SaleModified
- SaleCancelled

---

## Architecture

The project is structured using a layered architecture:

- Domain → Entities and business rules
- Application → Commands and Handlers (MediatR)
- Infrastructure (ORM) → Database access (Entity Framework Core)
- WebApi → Controllers and endpoints

---

## Tech Stack

- .NET
- ASP.NET Core Web API
- Entity Framework Core
- MediatR
- PostgreSQL
- Swagger (OpenAPI)

---

## Endpoints

| Method | Endpoint         | Description           |
|--------|------------------|----------------------|
| POST   | /api/sales       | Create a new sale     |
| GET    | /api/sales       | List all sales        |
| GET    | /api/sales/{id}  | Get sale by ID        |
| PUT    | /api/sales/{id}  | Update a sale         |
| DELETE | /api/sales/{id}  | Cancel a sale         |

---

## How to Run

1. Configure the connection string in appsettings.json

2. Run database migrations:
   dotnet ef database update

3. Run the application:
   dotnet run

4. Access Swagger:
   https://localhost:<port>/swagger

---

## Notes

- The project uses MediatR to handle application logic and maintain separation of concerns.
- Business rules (such as discount calculation) are applied during sale creation and update.
- Events are logged using ILogger instead of a message broker, as required by the challenge.

---

## Author

Developed as part of a technical evaluation.
