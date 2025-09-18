namespace Project_AMN.ApiRoutes;

public static class AdminEndpoints
{
    public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder app)
    {
        // CREATE USER
        app.MapPost("/api/admin/users", async (CreateUserDto dto, IMediator mediator) =>
        {
            var command = new CreateUserCommand(dto);
            var user = await mediator.Send(command);
            return user is null ? Results.BadRequest("Could not create user") : Results.Ok(user);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));
 
        // GET ALL USERS
        app.MapGet("/api/admin/users", async (IMediator mediator) =>
        {
            var users = await mediator.Send(new ListUsersQuery());
            return users.Any() ? Results.Ok(users) : Results.NotFound("No users found");
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));

        return app;
    }
}
