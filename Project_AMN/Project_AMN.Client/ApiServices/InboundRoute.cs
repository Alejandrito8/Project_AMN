using Project_AMN.Shared.DTO;
using System.Net.Http.Json;
namespace Project_AMN.Client.Services;

public class InboundService
{
    private readonly HttpClient _http;

    public InboundService(HttpClient http)
    {
        _http = http;
    }

public async Task<List<bool>> RegisterInboundAsync(string sku, int quantity)
{
    var response = await _http.PostAsJsonAsync(
        $"/api/inbounds/register?sku={sku}&quantity={quantity}", 
        value: (object?)null
    );

    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadFromJsonAsync<List<bool>>();
        return result ?? new List<bool>();
    }

    var error = await response.Content.ReadAsStringAsync();
    Console.WriteLine($"Fel vid registrering: {error}");
    return new List<bool>();
}

}