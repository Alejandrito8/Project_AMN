using Project_AMN.Shared.Models;

namespace Project_AMN.Shared.DTO
{
    /// <summary>
    /// Represents the data required to create a new order.
    /// </summary>
    public class OrderCreateDto
    {
        /// <summary>
        /// Gets or sets the total amount for the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the shipping address for the order.
        /// </summary>
        public string ShippingAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the list of order items included in the order.
        /// </summary>
        public List<OrderItemCreateDto> Items { get; set; } = new List<OrderItemCreateDto>();
    }

    /// <summary>
    /// Represents the data required to update the status of an existing order.
    /// </summary>
    public class OrderUpdateStatusDto
    {
        /// <summary>
        /// Gets or sets the ID of the order to update.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the new status of the order (e.g., "Created", "Sent", "Delivered").
        /// </summary>
        public OrderStatus Status { get; set; }

    }

    /// <summary>
    /// Represents the details of an order returned from the system.
    /// </summary>
    public class OrderResultDto
    {
        /// <summary>
        /// Gets or sets the unique ID of the order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the order.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the current status of the order.
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the shipping address for the order.
        /// </summary>
        public string ShippingAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the tracking number for the order, if any.
        /// </summary>
        public string? TrackingNumber { get; set; }
        public IEnumerable<OrderItemResultDto> Items { get; set; } = new List<OrderItemResultDto>();

    }

    /// <summary>
    /// Represents a single order item returned from the system.
    /// </summary>
    public class OrderItemResultDto
    {
        /// <summary>
        /// Gets or sets the ID of the associated article.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the name of the article.
        /// </summary>
        public string ArticleName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the article in the order.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the article at the time of the order.
        /// </summary>
        public decimal OrderPrice { get; set; }
    }

    /// <summary>
    /// Represents a single order item when creating a new order.
    /// </summary>
    public class OrderItemCreateDto
    {
        /// <summary>
        /// Gets or sets the ID of the article to include in the order.
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the article to order.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the article at the time of the order.
        /// </summary>
        public decimal OrderPrice { get; set; }
    }
};