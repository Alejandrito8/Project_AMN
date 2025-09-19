namespace Project_AMN.Shared.DTO;

public class ArticleSearchResultDto
{
    public IEnumerable<ArticleResultDto> Articles { get; set; } = new List<ArticleResultDto>();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}
