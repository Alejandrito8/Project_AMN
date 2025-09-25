namespace Project_AMN.ApiRoutes;

/// <summary>
/// Provides extension methods to register article-related API endpoints.
/// </summary>
public static class ArticleEndpoints
{
    /// <summary>
    /// Maps all article endpoints.
    /// </summary>
    public static IEndpointRouteBuilder MapArticleEndpoints(this IEndpointRouteBuilder app)
    {
        /// <summary>
        /// Retrieves a list of all articles.
        /// </summary>
        app.MapGet("api/articles", async (IMediator mediator) =>
        {
            var articles = await mediator.Send(new ListArticleQuery());
            return Results.Ok(articles);
        });
        
        /// <summary>
        /// Creates a new article.
        /// </summary>
        app.MapPost("api/articles", async (CreateArticleCommand articleCommand, IMediator mediator) =>
        {
            var article = await mediator.Send(articleCommand);
            return article is null ? Results.NotFound() : Results.Ok(article);
        });

        /// <summary>
        /// Updates an existing article by SKU.
        /// </summary>
        app.MapPut("api/articles/{sku}", async (
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

        /// <summary>
        /// Deletes an existing article by ID.
        /// </summary>
        app.MapDelete("api/articles/{id:int}", async (int Id, IMediator mediator) =>
        {
            var deleted = await mediator.Send(new DeleteArticleCommand(Id));
            return deleted
                ? Results.NoContent()
                : Results.NotFound($"Article with ID {Id} not found.");
        });

        /// <summary>
        /// Exports all articles to a CSV file.
        /// </summary>
        app.MapGet("api/articles/export", async (HttpResponse response, ApplicationDbContext db) =>
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
