
namespace Project_AMN.Handler
{
public class InboundArticleHandler : IRequestHandler<InboundArticleCommand, ArticleResultDto?>
{
    private readonly IInboundService _inboundService;

    public InboundArticleHandler(IInboundService inboundService)
    {
        _inboundService = inboundService;
    }

        public Task<ArticleResultDto?> Handle(InboundArticleCommand request, CancellationToken cancellationToken)
        {
            var result = _inboundService.RegisterInboundAsync(request.SKU, request.Quantity);
            return result.ContinueWith(t => t.Result ? new ArticleResultDto { SKU = request.SKU, Stock = request.Quantity } : null, cancellationToken);
        }
    }
}
