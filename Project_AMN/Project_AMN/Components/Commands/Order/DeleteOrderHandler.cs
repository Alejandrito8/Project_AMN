
namespace Project_AMN.Handler;

public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, bool>
{
    private readonly IOrderService _orderService;

    public DeleteOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<bool> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        return await _orderService.DeleteOrderAsync(request.OrderId);
    }
}






