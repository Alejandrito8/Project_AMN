namespace Project_AMN.Shared.Models
{
    /// <summary>
    /// Represents an order placed by a customer.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the unique identifier of the order.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the order was created.
        /// Defaults to UTC now.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the current status of the order.
        /// Examples: "Created", "Sent", "Delivered", "Cancelled".
        /// </summary>
        public OrderStatus Status { get; set; } = OrderStatus.Created;

        /// <summary>
        /// Gets or sets the total amount of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the shipping address for the order.
        /// </summary>
        public string ShippingAddress { get; set; } = "";

        /// <summary>
        /// Gets or sets the tracking number of the order shipment, if available.
        /// </summary>
        public string? TrackingNumber { get; set; }

        /// <summary>
        /// Gets or sets the collection of items included in the order.
        /// </summary>
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

        public enum OrderStatus
    {
        Created = 0,
        Sent = 1,
        Delivered = 2,
        Cancelled = 3,
        Completed = 4
    }
}
