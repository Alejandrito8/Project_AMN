
namespace Project_AMN.Commands;
public record ListArticleCommand() : IRequest<IEnumerable<ArticleResultDto>>;