namespace Project_AMN.ApiRoutes;
public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders", async (IOrderService service) =>
            Results.Ok(await service.GetAllOrdersAsync()));

        app.MapGet("/api/orders/{orderId}", async (int orderId, IOrderService service) =>
        {
            var order = await service.GetOrderByIdAsync(orderId);
            return order is null ? Results.NotFound() : Results.Ok(order);
        });

        app.MapPost("/api/orders", async (OrderCreateDto dto, IOrderService service) =>
            Results.Ok(await service.CreateOrderAsync(dto)));

        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        app.MapPut("/api/orders/{orderId}/status", async (int orderId, OrderUpdateStatusDto dto, IOrderService service) =>
        {
            dto.OrderId = orderId; // säkerställ att ID matchar route
            var updated = await service.UpdateOrderStatusAsync(dto);
            return updated is null ? Results.NotFound() : Results.Ok(updated);
        });

        app.MapDelete("/api/orders/{orderId}", async (int orderId, IOrderService service) =>
        {
            var deleted = await service.DeleteOrderAsync(orderId);
            return deleted ? Results.NoContent() : Results.NotFound();
        });

        app.MapPost("/api/orders/{orderId}/items", async (int orderId, OrderItemCreateDto dto, IOrderService service) =>
        {
            var addedItem = await service.AddItemToOrderAsync(orderId, dto.ArticleId, dto.Quantity, dto.OrderPrice);
            return addedItem is null ? Results.NotFound() : Results.Ok(addedItem);
        });

        app.MapGet("/api/orders/{orderId}/items", async (int orderId, IOrderService service) =>
        {
            var items = await service.GetOrderItemsAsync(orderId);
            return items.Any() ? Results.Ok(items) : Results.NotFound();
        });

        app.MapGet("/api/orders/search", async ([AsParameters] OrderSearchRequest request, IOrderService service) =>
        {
            OrderStatus? status = null;

            if (!string.IsNullOrWhiteSpace(request.Status) &&
                Enum.TryParse<OrderStatus>(request.Status, true, out var parsedStatus))
            {
                status = parsedStatus;
            }

            var results = await service.SearchOrdersAsync(status, request.FromDate, request.ToDate);
            return results.Any() ? Results.Ok(results) : Results.NotFound();
        });


        /// <summary>
        /// Exports all orders to a CSV file.
        /// </summary>
        app.MapGet("/api/orders/export", async (HttpResponse response, ApplicationDbContext db) =>
        {
            var orders = await db.Orders.Include(o => o.Items).ThenInclude(i => i.Article).ToListAsync();
            var fileBytes = ExportService.ExportOrders(orders);

            response.ContentType = "text/csv";
            response.Headers.Add("Content-Disposition", "attachment; filename=orders.csv");
            await response.Body.WriteAsync(fileBytes);
        });

        return app;
    }
}

