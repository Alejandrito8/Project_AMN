namespace Project_AMN.Commands;

/// <summary>
/// Command to update the status of an existing order.
/// </summary>
/// <param name="OrderId">The ID of the order to update.</param>
/// <param name="Status">The new status for the order.</param>
public record UpdateOrderStatusCommand(int OrderId, OrderStatus Status) 
    : IRequest<OrderResultDto?>;