namespace Project_AMN.Commands;

public record CreateArticleCommand(
string Name,
int Stock,
decimal Price,
string SKU,
string Location
) : IRequest<ArticleResultDto>;