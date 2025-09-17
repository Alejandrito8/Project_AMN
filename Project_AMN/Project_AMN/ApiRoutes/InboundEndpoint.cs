namespace Project_AMN.ApiRoutes;

public static class InboundEndpoints
{
    public static IEndpointRouteBuilder MapInboundEndpoints(this IEndpointRouteBuilder app)
    {
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

