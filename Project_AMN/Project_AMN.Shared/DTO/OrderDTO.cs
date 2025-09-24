using Project_AMN.Shared.Models;

namespace Project_AMN.Shared.DTO
{
    public class OrderCreateDto
    {
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
         public List<OrderItemCreateDto> Items { get; set; } = new List<OrderItemCreateDto>();
    }

    public class OrderUpdateStatusDto
    {
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
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the current status of the order.
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Gets or sets the total amount for the order.
        /// </summary>
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string? TrackingNumber { get; set; }
        public IEnumerable<OrderItemResultDto> Items { get; set; } = new List<OrderItemResultDto>();

    }
    public class OrderItemResultDto
    {
        /// <summary>
        /// Gets or sets the ID of the associated article.
        /// </summary>
        public int ArticleId { get; set; }
        public string ArticleName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal OrderPrice { get; set; }
    }
    public class OrderItemCreateDto
    {
        public int ArticleId { get; set; }
        public int Quantity { get; set; }
        public decimal OrderPrice { get; set; }
    }


};

