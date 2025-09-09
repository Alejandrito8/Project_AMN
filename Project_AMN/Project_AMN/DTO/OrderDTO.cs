namespace Project_AMN.DTOs;

public class OrderCreateDto
{
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
}

public class OrderUpdateStatusDto
{
    public int OrderId { get; set; }
    public string Status { get; set; } = string.Empty; // ex. "Sent", "Delivered"
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



