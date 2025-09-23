namespace Project_AMN.Shared.DTO
{
    /// <summary>
    /// Represents the filter criteria used when searching for orders.
    /// </summary>
    public class OrderSearchRequest
    {
        /// <summary>
        /// Gets or sets the status to filter orders by (e.g., "Created", "Sent", "Delivered").
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Gets or sets the start date for filtering orders.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the end date for filtering orders.
        /// </summary>
        public DateTime? ToDate { get; set; }
    }
};
