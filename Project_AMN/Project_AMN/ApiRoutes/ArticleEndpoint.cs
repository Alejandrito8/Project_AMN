using Project_AMN.Commands;

namespace Project_AMN.ApiRoutes;

public static class ArticleEndpoints
{
    public static IEndpointRouteBuilder MapArticleEndpoints(this IEndpointRouteBuilder app)
    {
        // // GET 
        app.MapGet("/api/articles", async (IMediator mediator) =>
        {
            var articles = await mediator.Send(new ListArticleCommand());
            return articles.Any() ? Results.Ok(articles) : Results.NotFound("No articles found.");
        });

        // POST
        app.MapPost("/api/articles", async (CreateArticleCommand articleCommand, IMediator mediator) =>
        {
            var article = await mediator.Send(articleCommand);
            return article is null ? Results.NotFound() : Results.Ok(article);
        });

        // PUT
app.MapPut("/api/articles/{sku}", async (
    string sku,
    ArticleUpdateDto dto,
    IMediator mediator) =>
{
    var command = new UpdateArticleCommand(sku, dto); 
    var updated = await mediator.Send(command);

    return updated is null 
        ? Results.NotFound($"Article with SKU {sku} not found.") 
        : Results.Ok(updated);
});


        // DELETE
        app.MapDelete("/api/articles/{id:int}", async (int Id, IMediator mediator) =>
        {
            var deleted = await mediator.Send(new DeleteArticleCommand(Id));
            return deleted
                ? Results.NoContent()
                : Results.NotFound($"Article with ID {Id} not found.");
        });


        return app;
    }
}
