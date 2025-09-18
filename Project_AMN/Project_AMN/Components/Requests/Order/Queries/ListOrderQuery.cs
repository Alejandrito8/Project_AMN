namespace Project_AMN.Queries;

public record ListOrderQuery() : IRequest<IEnumerable<OrderResultDto>>;
