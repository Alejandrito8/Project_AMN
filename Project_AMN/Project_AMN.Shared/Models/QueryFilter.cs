namespace Project_AMN.Shared.Models
{
    /// <summary>
    /// Represents filter criteria for querying articles or orders.
    /// </summary>
    public class QueryFilter
    {
        /// <summary>
        /// Gets or sets the search keyword.
        /// </summary>
        public string? Search { get; set; }

        /// <summary>
        /// Gets or sets the start date for the filter range.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the end date for the filter range.
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Gets or sets the page number for pagination.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination.
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
