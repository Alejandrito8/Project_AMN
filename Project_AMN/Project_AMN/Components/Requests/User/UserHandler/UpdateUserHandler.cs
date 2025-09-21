namespace Project_AMN.Handlers;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserService _userService;
    public UpdateUserHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await _userService.UpdateUserAsync(request.Id, request.UserDto);
    }
}