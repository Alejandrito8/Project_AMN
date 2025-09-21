namespace Project_AMN.ApiRoutes;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        // // GET 
        app.MapGet("/api/orders", async (IMediator mediator) =>
        {
            var orders = await mediator.Send(new ListOrderQuery());
            return orders.Any() ? Results.Ok(orders) : Results.NotFound("No orders found.");
        });
        
        // app.MapGet("/api/orders/{orderId:int}", async (int orderId, IMediator mediator) =>
        // {
        //     var order = await mediator.Send(new GetOrderByIdQuery(orderId));
        //     return order is null ? Results.NotFound($"Order with ID {orderId} not found.") : Results.Ok(order);
        // });

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

        app.MapGet("/orders/export", async (HttpResponse response, ApplicationDbContext db) =>
        {
            var orders = await db.Orders.ToListAsync();
            var fileBytes = ExportService.ExportOrders(orders);

            response.ContentType = "text/csv";
            response.Headers.Add("Content-Disposition", "attachment; filename=orders.csv");
            await response.Body.WriteAsync(fileBytes);
        });

        app.MapGet("/api/orders/search", async (
            [AsParameters] OrderSearchRequest request,
            IOrderService service) =>
        {
            var results = await service.SearchOrdersAsync(request.Status, request.FromDate, request.ToDate);

            if (!results.Any())
                return Results.NotFound("No matching orders found.");
            return Results.Ok(results);
        });
        return app;



    }
}
