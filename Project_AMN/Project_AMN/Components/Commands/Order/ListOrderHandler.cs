namespace Project_AMN.Handler;
public class ListOrderHandler
{
    private readonly IOrderService _orderService;

    public ListOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<IEnumerable<OrderResultDto?>> ExecuteAsync()
    {
        return await _orderService.GetAllOrdersAsync();
    }
}