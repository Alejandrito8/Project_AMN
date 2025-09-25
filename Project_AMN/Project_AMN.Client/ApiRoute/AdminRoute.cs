using System.Net.Http.Json;
using Project_AMN.Shared.DTO;
using System.Text.Json;    
using System.Net.Http;       



namespace Project_AMN.Client.ApiRoutes;

/// <summary>
/// Provides administrative functions for managing users via HTTP API.
/// </summary>
public class AdminRoute
{
    private readonly HttpClient _http;

    /// <summary>
    /// Initializes a new instance of the <see cref="AdminRoute"/> class.
    /// </summary>
    public AdminRoute(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Retrieves all users from the admin API.
    /// </summary>
public async Task<List<UserDto>> GetAllUsersAsync()
{
    var response = await _http.GetAsync("http://localhost:5000/api/admin/users");
    

    if (!response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"API returned {response.StatusCode}");
            Console.WriteLine($"Response content: {content}");
            return new List<UserDto>();
        }

    return await response.Content.ReadFromJsonAsync<List<UserDto>>() ?? new List<UserDto>();
}


    /// <summary>
    /// Creates a new user via the admin API.
    /// </summary>
    public async Task<UserDto?> CreateUserAsync(CreateUserDto dto)
    {
        var response = await _http.PostAsJsonAsync("http://localhost:5000/api/admin/users", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with creating User: {error}");
        return null;
    }

    /// <summary>
    /// Updates an existing user via the admin API.
    /// </summary>
    public async Task<UserDto?> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        var response = await _http.PutAsJsonAsync($"http://localhost:5000/api/admin/users/{id}", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with updating User: {error}");
        return null;
    }

    /// <summary>
    /// Deletes a user via the admin API.
    /// </summary>
    public async Task<bool> DeleteUserAsync(string id)
    {
        var response = await _http.DeleteAsync($"http://localhost:5000/api/admin/users/{id}");

        if (response.IsSuccessStatusCode)
        {
            return true;
        }

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return false;
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with deleting User: {error}");
        return false;
    }
}