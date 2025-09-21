

namespace Project_AMN.Handler;

public class CreateArticleHandler : IRequestHandler<CreateArticleCommand, ArticleResultDto?>

{
    private readonly IArticleService _articleService;
    public CreateArticleHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public Task<ArticleResultDto?> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var result = _articleService.CreateArticleAsync(new ArticleCreateDto
        {
            Name = request.Name,
            Stock = request.Stock,
            SKU = request.SKU,
            Price = request.Price,
            Location = request.Location
        });

        return result;
    }
}
