namespace Project_AMN.Shared.Models
{
    /// <summary>
    /// Represents an article/product in the system.
    /// </summary>
    public class Article
    {
        /// <summary>
        /// Gets or sets the unique identifier of the article.
        /// </summary>
        public int Id { get; set; }
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the name of the article.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stock keeping unit (SKU) of the article.
        /// </summary>
        public string SKU { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the available stock quantity of the article.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the price of the article.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the location of the article in the warehouse.
        /// </summary>
        public string Location { get; set; } = string.Empty;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
