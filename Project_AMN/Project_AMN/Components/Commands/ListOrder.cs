namespace Project_AMN.Commands;

public class ListOrdersCommand
{
    private readonly IOrderService _orderService;
    public ListOrdersCommand(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<IEnumerable<OrderResultDto>> ExecuteAsync()
    {
        return await _orderService.GetAllOrdersAsync();
    }
}