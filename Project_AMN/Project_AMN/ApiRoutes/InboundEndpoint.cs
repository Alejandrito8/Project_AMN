namespace Project_AMN.ApiRoutes;

/// <summary>
/// Provides extension methods to register inbound-related API endpoints.
/// </summary>
public static class InboundEndpoints
{
    /// <summary>
    /// Maps all inbound endpoints.
    /// </summary>
    public static IEndpointRouteBuilder MapInboundEndpoints(this IEndpointRouteBuilder app)
    {
        /// <summary>
        /// Updates the stock quantity of an article by SKU (inbound operation).
        /// </summary>
        app.MapPut("/api/inbound/{sku}", async (string sku, int quantity, IMediator mediator) =>
        {
            var command = new InboundArticleCommand(sku, quantity);
            var updated = await mediator.Send(command);

            return updated is null
                ? Results.NotFound($"Article with SKU {sku} not found.")
                : Results.Ok(updated);
        });

        return app;
    }
}
