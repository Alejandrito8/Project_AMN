namespace Project_AMN.Commands;
public record DeleteUserCommand(string Id) : IRequest<bool>;