
_______________________________________

---- Quick summary ----
_______________________________________

Project_AMN is a small marketplace / inventory management application built with .NET 8 and Blazor WebAssembly.
It provides:
    - A backend minimal API (ASP.NET Core) with MediatR-based handlers and 
      EF Core (SQLite) persistence and ASP.NET Core Identity for authentication and roles.
    - A Blazor WebAssembly client that consumes the backend via typed services.
    - Shared DTOs and models in Project_AMN.Shared to keep client/server contracts consistent.


What this repository contains: 
    - Project_AMN/Project_AMN.sln - Visual Studio solution.
    - Project_AMN/Project_AMN/Project_AMN - Server (minimal APIs, EF Core, Identity, MediatR handlers, services, migrations).
    - Project_AMN/Project_AMN/Project_AMN.Client - Blazor WebAssembly client (Pages, services).
    - Project_AMN/Project_AMN/Project_AMN.Shared - Shared DTOs and models (ArticleDTO, OrderDTO, UserDTO).
    - Migrations/ - EF Core migrations (creates Articles, Orders, Identity tables).


Technologies used:
    - C# / .NET 8
    - ASP.NET Core Minimal APIs
    - MediatR (CQRS-like pattern using Commands/Queries + Handlers)
    - Entity Framework Core (SQLite provider by default)
    - ASP.NET Core Identity (user & role management)
    - Blazor WebAssembly client
    - Simple CSV export utility for articles


Key projects / important files (what to open first):
- Server
    - Program.cs - app bootstrap (registers MediatR, EF Core, Identity, seeds default data).
    - ApiRoutes/*.cs - Maps HTTP endpoints to MediatR commands/queries.
    - Components/Requests/... - Commands/Queries and Handlers (MediatR).
    - Services/*.cs - Business logic, e.g., ArticleService, OrderService.
    - Data/ApplicationDbContext.cs - EF Core DbContext + Identity integration.
    - Migrations/* - DB schema creation.

- Client
    - Project_AMN.Client/Program.cs - Blazor startup (registers services and auth provider).
    - Project_AMN.Client/ApiServices/* - ArticleService, AdminService - thin HTTP wrappers.
    - Project_AMN.Client/Pages/* - Home.razor, Admin.razor, Orders.razor, Auth.razor - UI.

- Shared
    - Project_AMN.Shared/DTO/* - DTO classes (ArticleCreateDto, ArticleResultDto, UserDto, etc).
    

How it works:
    - Client pages (Blazor) call client-side services (e.g., ArticleService, AdminService) which use HttpClient to call backend endpoints.
    - Backend endpoints are defined via extension methods (ApiRoutes/*.cs) and typically Send(...) a MediatR Command or Query.
    - A Handler (in Components/Requests/.../Handler/...) receives the Command/Query and delegates to a Service 
      (e.g., ArticleService) which contains the business logic and talks to ApplicationDbContext.
    - ApplicationDbContext manages EF Core DbSet<Article>, DbSet<Order>, and inherits IdentityDbContext for user tables.
    - Responses use DTOs from Project_AMN.Shared.DTO to decouple internal persistence models from the API contract.

Architecture:
        Client (Blazor WASM UI)
                ↓
        API Endpoints (Minimal APIs)
                ↓
        CQRS Layer (Commands, Queries, Handlers)
                ↓
        Services (Business Logic)
                ↓
        EF Core + Identity
                ↓
        SQL Database

Features:
- Authentication & Identity
    * Register, login, 2FA, external providers
    * Password reset & email confirmation
    * Role-based access (Admin/User)

- Articles
    * CRUD operations + search
    * Results via DTOs

- Orders
    * CRUD operations
    * Filter & search with OrderSearchRequest

- Users
    * User creation & listing
    * Integrated with ASP.NET Identity

- Inbound
    * Handle incoming article/order data

- Admin
    * Restricted endpoints
    * CSV export service


API Endpoints Project_AMN/Project_AMN/Project_AMN/ApiRoutes/:  
- Articles
    * GET /articles - List articles
    * POST /articles - Create article
    * PUT /articles/{id} - Update article
    * DELETE /articles/{id} - Delete article

- Orders
    * GET /orders - List orders (with filters)
    * POST /orders - Create order
    * PUT /orders/{id} - Update order
    * DELETE /orders/{id} - Delete order

- Users
    * GET /users - List users
    * POST /users  Create user (admin only)

- Inbound
    * POST /inbound/articles - Process inbound article

- Admin
    * GET /admin/export - Export data as CSV (admin only)
    * POST /create
    * DELETE/users

__________________________________________________

-----Three User scenarios------
__________________________________________________
1. Admin creates a new employee account and adds an article
    * Actor: Admin (has role Admin)
    * Steps:
        1. Admin signs in (server-side Identity cookie created).
        2. In the admin UI (Admin.razor / Auth.razor), fills out a "Create New User" form, which calls AdminService.CreateUserAsync → POST /api/admin/users.
        3. Admin navigates to commerce dashboard and adds an article with ArticleService.CreateArticleAsync → POST /api/articles.
    * Outcome: New user created (stored in Identity tables), new article visible to other users.

2. Updates inventory inbound
    * Actor: Admin 
    * Steps:
        1. Employee identifies SKU (e.g., SKU-123).
        2. Calls PUT /api/inbound/{sku}?quantity=50 or uses the InboundService in client.
        3. Backend InboundArticleHandler increments article stock and returns updated ArticleResultDto.
    * Outcome: Article stock increased by given quantity; subsequent GET /api/articles shows updated stock.

3. Customer places an order
    * Actor: User / End-user
    * Steps:
        1. User selects items (client would calculate TotalAmount).
        2. Client calls POST /api/orders with an OrderCreateDto (TotalAmount, ShippingAddress, etc).
        3. CreateOrderHandler delegates to OrderService.CreateOrderAsync which creates an order row in DB.
        4. Admin or user later can update order status via PUT /api/orders/{orderId}/status.
    * Outcome: New order created, visible in GET /api/orders.

