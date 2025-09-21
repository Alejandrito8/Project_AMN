namespace Project_AMN.ApiRoutes;

public static class ArticleEndpoints
{
    public static IEndpointRouteBuilder MapArticleEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapGet("/api/articles", async (IMediator mediator) =>
        {
            var articles = await mediator.Send(new ListArticleQuery());
            return articles.Any() ? Results.Ok(articles) : Results.NotFound("No articles found.");
        });

        // app.MapGet("/api/articles/{id:int}", async (int id, IMediator mediator) =>
        // {
        //     var article = await mediator.Send(new GetArticleByIdQuery(id));
        //     return article is null ? Results.NotFound($"Article with ID {id} not found.") : Results.Ok(article);
        // });
        
        app.MapPost("/api/articles", async (CreateArticleCommand articleCommand, IMediator mediator) =>
        {
            var article = await mediator.Send(articleCommand);
            return article is null ? Results.NotFound() : Results.Ok(article);
        });


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

        app.MapDelete("/api/articles/{id:int}", async (int Id, IMediator mediator) =>
        {
            var deleted = await mediator.Send(new DeleteArticleCommand(Id));
            return deleted
                ? Results.NoContent()
                : Results.NotFound($"Article with ID {Id} not found.");
        });

        app.MapGet("/articles/export", async (HttpResponse response, ApplicationDbContext db) =>
        {
            var articles = await db.Articles.ToListAsync();
            var fileBytes = ExportService.ExportArticles(articles);

            response.ContentType = "text/csv";
            response.Headers.Add("Content-Disposition", "attachment; filename=articles.csv");
            await response.Body.WriteAsync(fileBytes);
        });

        return app;
    }
}
