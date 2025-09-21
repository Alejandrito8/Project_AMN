namespace Project_AMN.Commands;

public record InboundArticleCommand(string SKU, int Quantity) : IRequest<ArticleResultDto?>;
