// public record DeleteOrderCommand(
//     int OrderId
// ) : IRequest<OrderResultDto>;

public record DeleteOrderCommand(int OrderId) : IRequest<bool>;


