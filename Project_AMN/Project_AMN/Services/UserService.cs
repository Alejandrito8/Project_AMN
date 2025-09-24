namespace Project_AMN.Services;

/// <summary>
/// Provides services for managing users, including creation, update, and retrieval.
/// </summary>
public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    /// <summary>
    /// Creates a new user and assigns roles.
    /// </summary>
    public async Task<UserDto?> CreateUserAsync(CreateUserDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Create role if it does not exist
        if (!await _roleManager.RoleExistsAsync(dto.Role))
        {
            await _roleManager.CreateAsync(new IdentityRole(dto.Role));
        }

        await _userManager.AddToRoleAsync(user, dto.Role);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Roles = new List<string> { dto.Role }
        };
    }

    /// <summary>
    /// Retrieves all users with their roles.
    /// </summary>
    public async Task<IReadOnlyList<UserDto>> GetAllUsersAsync()
    {
        var users = _userManager.Users.ToList();

        var list = new List<UserDto>();
        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            list.Add(new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email!,
                Roles = roles.ToList()
            });
        }

        return list;
    }

    /// <summary>
    /// Updates an existing user and their roles.
    /// </summary>
    public async Task<UserDto?> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return null;

        // Update user's basic information
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.UserName = dto.Email;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        // Update roles
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Remove all old roles
        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // Add new roles from DTO
        foreach (var role in dto.Roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));
        }
        if (dto.Roles.Any())
            await _userManager.AddToRolesAsync(user, dto.Roles);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            Roles = (await _userManager.GetRolesAsync(user)).ToList()
        };
    }
}