namespace Project_AMN.Commands;
public class UpdateOrderStatusCommand
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusCommand(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderResultDto?> ExecuteAsync(OrderUpdateStatusDto dto)
    {
        return await _orderService.UpdateOrderStatusAsync(dto);
    }
}
