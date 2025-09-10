
namespace Project_AMN.Commands;

public record CreateOrderCommand(
    string Status,
    decimal TotalAmount,    
    string ShippingAddress
) : IRequest<OrderResultDto>;

