namespace Project_AMN.Commands;
public record CreateUserCommand(CreateUserDto Dto) : IRequest<UserDto?>;



