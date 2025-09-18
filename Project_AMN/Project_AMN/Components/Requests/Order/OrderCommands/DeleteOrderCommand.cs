namespace Project_AMN.Commands;
public record DeleteOrderCommand(int OrderId) : IRequest<bool>;


