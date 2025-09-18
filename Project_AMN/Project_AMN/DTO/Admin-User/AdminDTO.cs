namespace Project_AMN.DTO;


/// Used when an Admin approves or manages users
public class AdminUserDto
{
    public string UserId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsApproved { get; set; }
}
