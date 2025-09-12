
namespace Project_AMN.Handler;

public class ListArticleHandler : IRequestHandler<ListArticleCommand, IEnumerable<ArticleResultDto>>
{
    private readonly IArticleService _articleService;

    public ListArticleHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }
    public async Task<IEnumerable<ArticleResultDto>> Handle(ListArticleCommand request, CancellationToken cancellationToken)
    {
        return await _articleService.GetAllArticlesAsync();
    }

}
