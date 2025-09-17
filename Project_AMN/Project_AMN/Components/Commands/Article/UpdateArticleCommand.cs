namespace Project_AMN.Commands;

public record UpdateArticleCommand(
       string SKU,
       ArticleUpdateDto Dto
 
) : IRequest<ArticleResultDto>;