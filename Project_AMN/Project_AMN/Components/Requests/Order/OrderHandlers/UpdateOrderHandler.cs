namespace Project_AMN.Handler;

/// <summary>
/// Handles updating the status of an existing order.
/// </summary>
public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, OrderResultDto?>
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderResultDto?> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _orderService.UpdateOrderStatusAsync(
            new OrderUpdateStatusDto
            {
                OrderId = request.OrderId,
                Status = request.Status
            }
        );

        return result;
    }
}
