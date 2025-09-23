namespace Project_AMN.Shared.Models
{
    /// <summary>
    /// Represents a single item within an order.
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the order this item belongs to.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the associated order object.
        /// </summary>
        public Order Order { get; set; } = null!;

        /// <summary>
        /// Gets or sets the identifier of the article/product.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the associated article/product object.
        /// </summary>
        public Article Article { get; set; } = null!;

        /// <summary>
        /// Gets or sets the quantity of the article in the order.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the article at the time of the order.
        /// </summary>
        public decimal OrderPrice { get; set; }
    }
}
