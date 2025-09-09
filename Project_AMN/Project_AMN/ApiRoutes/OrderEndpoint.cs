

namespace Project_AMN.ApiRoutes;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders", async (IOrderService service) =>
        {
            var orders = await service.GetAllOrdersAsync();
            return Results.Ok(orders);
        });

        app.MapGet("/api/orders/{id:int}", async (int id, IOrderService service) =>
        {
            var order = await service.GetOrderByIdAsync(id);
            return order is null ? Results.NotFound() : Results.Ok(order);
        });

        app.MapPost("/api/orders", async (OrderCreateDto dto, IOrderService service) =>
        {
            var created = await service.CreateOrderAsync(dto);
            return Results.Created($"/api/orders/{created.OrderId}", created);
        });

        app.MapPut("/api/orders/{id:int}/status", async (int id, OrderUpdateStatusDto dto, IOrderService service) =>
        {
            if (id != dto.OrderId) return Results.BadRequest("Mismatched order ID");

            var updated = await service.UpdateOrderStatusAsync(dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });

        app.MapDelete("/api/orders/{id:int}", async (int id, IOrderService service) =>
        {
            await service.DeleteOrderAsync(id);
            return Results.NoContent();
        });

        return app;
    }
}
