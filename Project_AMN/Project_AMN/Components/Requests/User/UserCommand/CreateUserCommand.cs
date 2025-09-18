namespace Project_AMN.Commands;
public record CreateUserCommand(CreateUserDto UserDto) : IRequest<UserDto?>;