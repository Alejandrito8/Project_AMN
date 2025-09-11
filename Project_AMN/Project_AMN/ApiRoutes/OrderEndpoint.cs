using Project_AMN.Commands;

namespace Project_AMN.ApiRoutes;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        // // GET 
        app.MapGet("/api/orders", async (IMediator mediator) =>
        {
            var orders = await mediator.Send(new ListOrderCommand());
            return orders.Any() ? Results.Ok(orders) : Results.NotFound("No orders found.");
        });

        // POST
        app.MapPost("/api/orders", async (CreateOrderCommand orderCommand, IMediator mediator) =>
        {
            var order = await mediator.Send(orderCommand);
            return order is null ? Results.NotFound() : Results.Ok(order);
        });

        // PUT
        app.MapPut("/api/orders/{orderId:int}/status", async (
                   int orderId,
                   UpdateOrderStatusCommand request,
                   IMediator mediator) =>
               {
                   var command = new UpdateOrderStatusCommand(orderId);
                   var updated = await mediator.Send(command);
                   return updated is null ? Results.NotFound($"Order with ID {orderId} not found.") : Results.Ok(updated);
               });

        // DELETE
        app.MapDelete("/api/orders/{orderId:int}", async (int orderId, IMediator mediator) =>
        {
            var deleted = await mediator.Send(new DeleteOrderCommand(orderId));
            return deleted
                ? Results.NoContent()
                : Results.NotFound($"Order with ID {orderId} not found.");
        });


        return app;
    }
}
