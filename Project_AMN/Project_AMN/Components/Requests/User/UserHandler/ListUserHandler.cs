namespace Project_AMN.Handler;
public class ListUsersHandler : IRequestHandler<ListUsersQuery, IReadOnlyList<UserDto>>
{
    private readonly IUserService _userService;
    public ListUsersHandler(IUserService userService)
    {
        _userService = userService;
    }

    public async Task<IReadOnlyList<UserDto>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        return await _userService.GetAllUsersAsync();
    }
}