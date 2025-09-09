namespace Project_AMN.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new inbound record.
    /// Used when the client sends data to create a new article/order.
    /// </summary>
    public class InboundCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string Location { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object (DTO) for updating the status of an inbound order.
    /// Used when the client updates an order’s status.
    /// </summary>
    public class InboundUpdateStatusDto
    {
        public int OrderId { get; set; }
        public string Status { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object (DTO) for returning inbound data to the client.
    /// Used when sending results back to the user.
    /// </summary>
    public class InboundResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string Location { get; set; } = string.Empty;
    }
}