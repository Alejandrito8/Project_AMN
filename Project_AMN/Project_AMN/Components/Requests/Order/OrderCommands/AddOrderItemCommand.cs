namespace Project_AMN.Commands;

/// <summary>
/// Command for adding an item to an existing order.
/// </summary>
public record AddOrderItemCommand(int OrderId, int ArticleId, int Quantity, decimal OrderPrice) 
    : IRequest<OrderItemResultDto?>;