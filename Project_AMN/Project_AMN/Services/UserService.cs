namespace Project_AMN.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

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

        // Skapa roll om den inte finns
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

    public async Task<UserDto?> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null) return null;

        // Uppdatera användarens grunddata
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Email = dto.Email;
        user.UserName = dto.Email;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));

        // Uppdatera roller
        var currentRoles = await _userManager.GetRolesAsync(user);

        // Ta bort alla gamla roller
        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // Lägg till nya roller från DTO
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

    // public async Task<bool> DeleteUserAsync(string id)
    // {
    //     var user = await _userManager.FindByIdAsync(id);
    //     if (user == null) return false;

    //     var result = await _userManager.DeleteAsync(user);
    //     return result.Succeeded;
    // }

}