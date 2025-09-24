namespace Project_AMN.Handler;

public class ListArticleHandler : IRequestHandler<ListArticleQuery, IEnumerable<ArticleResultDto>>
{
    private readonly IArticleService _articleService;

    public ListArticleHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }
    public async Task<IEnumerable<ArticleResultDto>> Handle(ListArticleQuery request, CancellationToken cancellationToken)
    {
        return await _articleService.GetAllArticlesAsync();
    }

}