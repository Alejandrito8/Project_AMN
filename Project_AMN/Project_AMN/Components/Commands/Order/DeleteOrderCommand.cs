public record DeleteOrderCommand(int OrderId) : IRequest<bool>;


