namespace Project_AMN.Handlers;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto?>
{
    private readonly IUserService _userService;
    public CreateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDto?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.CreateUserAsync(request.UserDto);
    }
}