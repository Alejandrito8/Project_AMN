

// namespace Project_AMN.Handler;

// public class AddOrderItemHandler : IRequestHandler<AddOrderItemCommand, OrderItemResultDto>
// {
//     private readonly IOrderService _orderService;

//     public AddOrderItemHandler(IOrderService orderService)
//     {
//         _orderService = orderService;
//     }

//     public async Task<OrderItemResultDto> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
//     {
//         var result = await _orderService.AddItemToOrderAsync(request.OrderId, request.ArticleId, request.Quantity, request.OrderPrice);
//         return result;
//     }
// }
