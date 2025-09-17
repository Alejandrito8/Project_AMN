namespace Project_AMN.Commands;

public record DeleteArticleCommand(int Id) : IRequest<bool>;