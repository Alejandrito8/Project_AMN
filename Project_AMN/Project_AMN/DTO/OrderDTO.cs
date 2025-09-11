namespace Project_AMN.DTO;

public class OrderCreateDto
{
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
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



