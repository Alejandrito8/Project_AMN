namespace Project_AMN.Shared.Models
{
    public class Order
    {
        public int OrderId { get; set; }
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
        public string ShippingAddress { get; set; } = "";
        public string? TrackingNumber { get; set; }
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
