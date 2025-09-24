using System.Net.Http.Json;
using Project_AMN.Shared.DTO;

namespace Project_AMN.Client.ApiRoutes;

/// <summary>
/// Provides client-side methods to register inbound articles via the API.
/// </summary>
public class InboundRoute
{
    private readonly HttpClient _http;

    /// <summary>
    /// Initializes a new instance of the <see cref="InboundRoute"/> class.
    /// </summary>
    public InboundRoute(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Registers an inbound quantity for an article using its SKU.
    /// </summary>
    /// <param name="sku">The SKU of the article.</param>
    /// <param name="quantity">The quantity to register as inbound.</param>
    /// <returns>The updated article data, or null if the operation failed.</returns>
    public async Task<ArticleResultDto?> RegisterInboundAsync(string sku, int quantity)
    {
        var response = await _http.PutAsJsonAsync($"/api/inbound/{sku}?quantity={quantity}",
                                                   value: (object?)null);

        if (response.IsSuccessStatusCode)
        {
            var article = await response.Content.ReadFromJsonAsync<ArticleResultDto>();
            return article;
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while registering inbound: {error}");
        return null;
    }
}