namespace Project_AMN.Interfaces;

/// <summary>
/// Defines operations for managing users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Creates a new user.
    /// </summary>
    Task<UserDto?> CreateUserAsync(CreateUserDto dto);

    /// <summary>
    /// Retrieves all users.
    /// </summary>
    Task<IReadOnlyList<UserDto>> GetAllUsersAsync();

    // /// <summary>
    // /// Deletes a user by ID.
    // /// </summary>
    // Task<bool> DeleteUserAsync(string id);

    /// <summary>
    /// Updates an existing user.
    /// </summary>
    Task<UserDto?> UpdateUserAsync(string Id, UpdateUserDto dto);
}