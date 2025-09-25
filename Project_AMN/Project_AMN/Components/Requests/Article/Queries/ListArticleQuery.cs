namespace Project_AMN.Queries;
public record ListArticleQuery() : IRequest<IEnumerable<ArticleResultDto>>;