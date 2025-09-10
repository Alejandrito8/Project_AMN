public class DeleteOrderHandler
{
    private readonly IOrderService _orderService;
    public DeleteOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task ExecuteAsync(int orderId)
    {
        // Här kan du lägga till extra logik: validering, logging osv
        await _orderService.DeleteOrderAsync(orderId);
    }
}
