namespace Project_AMN.Shared.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Created"; // Created, Sent, Delivered, Cancelled
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = "";
        public string? TrackingNumber { get; set; }
    }
}