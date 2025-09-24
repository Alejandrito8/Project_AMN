
namespace Project_AMN.Commands;

/// <summary>
/// Command for creating a new order.
/// </summary>
public record CreateOrderCommand(
    decimal TotalAmount,
    string ShippingAddress
) : IRequest<OrderResultDto?>;
