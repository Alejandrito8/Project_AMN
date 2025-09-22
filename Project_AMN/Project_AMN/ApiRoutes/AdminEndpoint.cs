namespace Project_AMN.ApiRoutes;

public static class AdminEndpoints
{
    public static IEndpointRouteBuilder MapAdminEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/api/admin/users", async (CreateUserDto dto, IMediator mediator) =>
        {
            var command = new CreateUserCommand(dto);
            var user = await mediator.Send(command);
            return user is null ? Results.BadRequest("Could not create user") : Results.Ok(user);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));


        app.MapGet("/api/admin/users", async (IMediator mediator) =>
        {
            var users = await mediator.Send(new ListUsersQuery());
            return Results.Ok(users);
        })
        .RequireAuthorization(policy => policy.RequireRole("Admin"));

        app.MapPut("/api/admin/users/{id}", async (string id, UpdateUserDto dto, IMediator mediator) =>
        {
            var command = new UpdateUserCommand(id, dto);
            var updated = await mediator.Send(command);
            return updated is null ? Results.NotFound($"User with ID {id} not found.") : Results.Ok(updated);
        })
         .RequireAuthorization(policy => policy.RequireRole("Admin"));;
        app.MapDelete("/api/admin/users/{id}", async (string id, IMediator mediator) =>
        {
            var deleted = await mediator.Send(new DeleteUserCommand(id));
            return deleted ? Results.NoContent() : Results.NotFound($"User with ID {id} not found.");
        })
         .RequireAuthorization(policy => policy.RequireRole("Admin"));;

        return app;
    }
}
