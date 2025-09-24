namespace Project_AMN.Handler;

/// <summary>
/// Handles adding a new item to an existing order.
/// </summary>
public class AddOrderItemHandler : IRequestHandler<AddOrderItemCommand, OrderItemResultDto?>
{
    private readonly IOrderService _orderService;

    /// <summary>
    /// Initializes a new instance of the <see cref="AddOrderItemHandler"/> class.
    /// </summary>
    public AddOrderItemHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Handles the command to add a new item to an order.
    /// </summary>
    public async Task<OrderItemResultDto?> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _orderService.AddItemToOrderAsync(
            request.OrderId,
            request.ArticleId,
            request.Quantity,
            request.OrderPrice
        );

        return result;
    }
}
