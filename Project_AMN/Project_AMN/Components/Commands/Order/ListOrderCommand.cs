namespace Project_AMN.Commands;

public record ListOrderCommand() : IRequest<IEnumerable<OrderResultDto>>;
