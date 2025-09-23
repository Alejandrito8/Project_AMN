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
        public string Status { get; set; } = "Created"; // ex. "Sent", "Delivered"
    }

    public class OrderResultDto
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string? TrackingNumber { get; set; }
    }
    public class OrderItemResultDto
    {
        public int Id { get; set; }
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

