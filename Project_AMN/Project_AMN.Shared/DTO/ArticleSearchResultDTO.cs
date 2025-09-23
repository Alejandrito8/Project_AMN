namespace Project_AMN.Shared.DTO
{
    /// <summary>
    /// Represents the result of an article search, including paging information.
    /// </summary>
    public class ArticleSearchResultDto
    {
        /// <summary>
        /// Gets or sets the list of articles returned by the search.
        /// </summary>
        public IEnumerable<ArticleResultDto> Articles { get; set; } = new List<ArticleResultDto>();

        /// <summary>
        /// Gets or sets the total number of articles matching the search criteria.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the current page number.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the number of articles per page.
        /// </summary>
        public int PageSize { get; set; }
    }
};
