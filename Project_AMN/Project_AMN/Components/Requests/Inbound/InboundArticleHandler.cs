namespace Project_AMN.Handler
{
    public class InboundArticleHandler : IRequestHandler<InboundArticleCommand, ArticleResultDto?>
    {
        private readonly IInboundService _inboundService;
        private readonly IArticleService _articleService;

        public InboundArticleHandler(IInboundService inboundService, IArticleService articleService)
        {
            _inboundService = inboundService;
            _articleService = articleService;
        }

        public async Task<ArticleResultDto?> Handle(InboundArticleCommand request, CancellationToken cancellationToken)
        {
            // Registrera inleveransen via InboundService
            var success = await _inboundService.RegisterInboundAsync(request.SKU, request.Quantity);
            if (!success) return null;

            // Hämta och returnera uppdaterad artikel
            var articles = await _articleService.GetAllArticlesAsync();
            var updatedArticle = articles.FirstOrDefault(a => a.SKU == request.SKU);
            return updatedArticle;
        }
    }
}