namespace Project_AMN.Handler;
public class UpdateOrderStatusHandler
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderResultDto?> ExecuteAsync(OrderUpdateStatusDto dto)
    {
        return await _orderService.UpdateOrderStatusAsync(dto);
    }
}
