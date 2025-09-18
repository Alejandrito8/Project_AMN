namespace Project_AMN.DTO;


public class OrderSearchRequest
{
    public string? Status { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
