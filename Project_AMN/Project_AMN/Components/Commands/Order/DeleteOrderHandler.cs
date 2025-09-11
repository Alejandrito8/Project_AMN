// public class DeleteOrderHandler
// {
// private readonly IOrderService _orderService;
// public DeleteOrderHandler(IOrderService orderService)
// {
//     _orderService = orderService;
// }
// public async Task ExecuteAsync(int orderId)
// {
//     // Här kan du lägga till extra logik: validering, logging osv
//     await _orderService.DeleteOrderAsync(orderId);
// }


// }

// public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Unit>
// {
//     private readonly IOrderService _orderService;

//     public DeleteOrderHandler(IOrderService orderService)
//     {
//         _orderService = orderService;
//     }

//     public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
//     {
//         await _orderService.DeleteOrderAsync(request.OrderId);
//         return Unit.Value; // MediatR behöver alltid en returtyp
//     }
// }
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






