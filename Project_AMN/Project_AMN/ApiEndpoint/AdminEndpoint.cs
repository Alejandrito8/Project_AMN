namespace Project_AMN.ApiRoutes;

/// <summary>
/// Provides extension methods to register admin-related API endpoints.
/// </summary>
public static class AdminEndpoints
{
    /// <summary>
    /// Maps all admin endpoints related to user management.
    /// </summary>
    public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder app)
    {
        /// <summary>
        /// Creates a new user.
        /// </summary>
        app.MapPost("/api/admin/users", async (CreateUserDto dto, IMediator mediator) =>
        {
            var command = new CreateUserCommand(dto);
            var user = await mediator.Send(command);
            return user is null ? Results.BadRequest("Could not create user") : Results.Ok(user);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        app.MapGet("/api/admin/users", async (IMediator mediator) =>
        {
            var users = await mediator.Send(new ListUsersQuery());
            return Results.Ok(users);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));

        /// <summary>
        /// Updates an existing user by ID.
        /// </summary>
        app.MapPut("/api/admin/users/{id}", async (string id, UpdateUserDto dto, IMediator mediator) =>
        {
            var command = new UpdateUserCommand(id, dto);
            var updated = await mediator.Send(command);
            return updated is null ? Results.NotFound($"User with ID {id} not found.") : Results.Ok(updated);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));

        /// <summary>
        /// Deletes an existing user by ID.
        /// </summary>
        app.MapDelete("/api/admin/users/{id}", async (string id, IMediator mediator) =>
        {
            var deleted = await mediator.Send(new DeleteUserCommand(id));
            return deleted ? Results.NoContent() : Results.NotFound($"User with ID {id} not found.");
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));

        return app;
    }
}