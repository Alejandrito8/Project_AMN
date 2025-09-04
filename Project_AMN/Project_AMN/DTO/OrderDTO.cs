// namespace Project_AMN.DTOs;

// public class OrderCreateDto
// {
//     public decimal TotalAmount { get; set; }
//     public string ShippingAddress { get; set; } = string.Empty;
// }

// public class OrderUpdateStatusDto
// {
//     public int OrderId { get; set; }
//     public string Status { get; set; } = string.Empty; // ex. "Sent", "Delivered"
// }

// public class OrderResultDto
// {
//     public int OrderId { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public string Status { get; set; } = string.Empty;
//     public decimal TotalAmount { get; set; }
//     public string ShippingAddress { get; set; } = string.Empty;
//     public string? TrackingNumber { get; set; }
// }


// OBS: Model (Order) → hur databasen ser ut internt

/*DTO (OrderCreateDto, OrderResultDto) → vad klienten får skicka in/få ut

 På så sätt kan du:
 Skydda känsliga fält
 Hindra fusk/injektioner
 Ha stabila API-kontrakt även om databasen ändras*/
