using System.Net.Http.Json;
using Project_AMN.Shared.DTO;

namespace Project_AMN.Client.Services;

public class AdminService
{
    private readonly HttpClient _http;

    public AdminService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var response = await _http.GetAsync("/api/admin/users");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<UserDto>>() ?? new List<UserDto>();
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return new List<UserDto>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with retrieving Users: {error}");
        return new List<UserDto>();
    }

    public async Task<UserDto?> CreateUserAsync(CreateUserDto dto)
    {
        var response = await _http.PostAsJsonAsync("/api/admin/users", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with creating User: {error}");
        return null;
    }
    public async Task<UserDto?> UpdateUserAsync(string id, UpdateUserDto dto)
    {
        var response = await _http.PutAsJsonAsync($"/api/admin/users/{id}", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UserDto>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with updating User: {error}");
        return null;
    }
    public async Task<bool> DeleteUserAsync(string id)
    {
        var response = await _http.DeleteAsync($"/api/admin/users/{id}");

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
