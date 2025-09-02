
namespace Project_AMN.ApiRoutes;
public static class ApiRoutes
{
    public static IEndpointRouteBuilder MapApiRoutes(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(pattern: "/api/articles", () => new[]
        {
            new { id = 1, name = "Skruv", sku = "A-001", stock = 100, location = "A1" },
            new { id = 2, name = "Spik",  sku = "A-201", stock = 2300, location = "B4" }
        })        .WithName("GetArticles")
            .WithTags("Articles")
            .WithMetadata(new Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute())
            .Produces(statusCode: StatusCodes.Status200OK)
            .Produces(statusCode: StatusCodes.Status401Unauthorized)
            .Produces(statusCode: StatusCodes.Status403Forbidden);
        return endpoints;
    }

}               
