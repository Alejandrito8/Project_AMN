namespace   Project_AMN.Interfaces;

public interface IUserService
{
    Task<UserDto?> CreateUserAsync(CreateUserDto dto);
    Task<IReadOnlyList<UserDto>> GetAllUsersAsync();
    // Task<bool> DeleteUserAsync(string id);
    Task<UserDto?> UpdateUserAsync(string Id, UpdateUserDto dto);
}