// namespace Project_AMN.Handler;
// public class ListOrderHandler
// {
//     private readonly IOrderService _orderService;

//     public ListOrderHandler(IOrderService orderService)
//     {
//         _orderService = orderService;
//     }
//     public async Task<IEnumerable<OrderResultDto?>> ExecuteAsync()
//     {
//         return await _orderService.GetAllOrdersAsync();
//     }
// }


namespace Project_AMN.Handler;

public class ListOrderHandler : IRequestHandler<ListOrderCommand, IEnumerable<OrderResultDto>>
{
    private readonly IOrderService _orderService;

    public ListOrderHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<IEnumerable<OrderResultDto>> Handle(ListOrderCommand request, CancellationToken cancellationToken)
    {
        return await _orderService.GetAllOrdersAsync();
    }
}
