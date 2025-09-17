
namespace Project_AMN.Handler;

public class UpdateArticleHandler : IRequestHandler<UpdateArticleCommand, ArticleResultDto>
{
    private readonly IArticleService _articleService;

    public UpdateArticleHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<ArticleResultDto> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var result = await _articleService.UpdateArticleAsync(request.Dto, request.SKU);
        return result;

    }


}
