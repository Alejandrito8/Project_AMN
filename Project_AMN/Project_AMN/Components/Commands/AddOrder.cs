namespace Project_AMN.Commands;

public class CreateOrderCommand
{
    private readonly IOrderService _orderService;

    public CreateOrderCommand(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderResultDto> ExecuteAsync(OrderCreateDto dto)
    {
        // här skulle du kunna lägga extra validering, logging osv
        return await _orderService.CreateOrderAsync(dto);
    }
}

