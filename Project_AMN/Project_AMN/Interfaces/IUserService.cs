namespace   Project_AMN.Interfaces;

public interface IUserService
{
    Task<UserDto?> CreateUserAsync(CreateUserDto dto);
    Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
}