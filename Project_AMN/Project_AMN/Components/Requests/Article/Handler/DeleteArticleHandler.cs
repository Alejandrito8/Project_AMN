
namespace Project_AMN.Handler;

public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand, bool>
{
    private readonly IArticleService _articleService;

    public DeleteArticleHandler(IArticleService articleService)
    {
        _articleService = articleService;
    }

    public async Task<bool> Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        return await _articleService.DeleteArticleAsync(request.Id);
    }

}




