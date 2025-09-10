namespace Project_AMN.Commands;


public record ListOrderCommand(
    int OrderId,
    DateTime CreatedAt,
    string Status,
    decimal TotalAmount,
    string ShippingAddress,
    string? TrackingNumber
)
 : IRequest<OrderResultDto>;

    