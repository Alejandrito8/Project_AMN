using MediatR;

namespace Project_AMN.Commands;

public record UpdateOrderStatusCommand(
    int OrderId
) : IRequest<OrderResultDto>;


