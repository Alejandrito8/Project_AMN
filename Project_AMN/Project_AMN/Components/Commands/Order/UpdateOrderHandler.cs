
namespace Project_AMN.Handler;
public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, OrderResultDto>
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderResultDto> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var result = await _orderService.UpdateOrderStatusAsync(request.OrderId);
        return result;
    }
}
