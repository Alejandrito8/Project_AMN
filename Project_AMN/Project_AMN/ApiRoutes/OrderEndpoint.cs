using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Project_AMN.Commands;

namespace Project_AMN.ApiRoutes;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        // // GET - hämta alla orders via Mediator
        // app.MapGet("/api/orders", async (IMediator mediator, ListOrderCommand command) =>
        // {
        //     var orders = await mediator.Send(command);
        //     return orders is null ? Results.NotFound("No orders found.") : Results.Ok(orders);
        // });

        // POST
        app.MapPost("/api/orders/{id:int}", async (CreateOrderCommand orderCommand, IMediator mediator) =>
        {
            var order = await mediator.Send(orderCommand);
            return order is null ? Results.NotFound() : Results.Ok(order);
        });

        // // PUT
        // app.MapPut("/api/orders/{id:int}/status", async (UpdateOrderStatusCommand orderCommand, IMediator mediator) =>
        // {
        //     var updated = await mediator.Send(orderCommand);
        //     return updated is null ? Results.NotFound() : Results.Ok(updated);
        // });

        // // DELETE
        // app.MapDelete("/api/orders/{id:int}", async ( DeleteOrderCommand orderCommand, IMediator mediator) =>
        // {
        //     await mediator.Send(orderCommand);
        //     return Results.NoContent();
        // });

        return app;
    }
}
