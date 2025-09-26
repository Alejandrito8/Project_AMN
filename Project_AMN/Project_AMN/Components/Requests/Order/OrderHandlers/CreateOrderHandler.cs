namespace Project_AMN.Handler;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderResultDto?>
{
    private readonly IOrderService _orderService;

    public CreateOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OrderResultDto?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await _orderService.CreateOrderAsync(new OrderCreateDto
        {
            TotalAmount = request.TotalAmount,
            ShippingAddress = request.ShippingAddress
        });

        return result;
    }
}