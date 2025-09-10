public record DeleteOrderCommand(
    int OrderId
) : IRequest<OrderResultDto>;

