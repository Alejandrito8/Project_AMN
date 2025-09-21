namespace Project_AMN.Commands;
public record UpdateUserCommand(string Id, UpdateUserDto UserDto) : IRequest<UserDto?>;