using System.Net.Http.Json;
using Project_AMN.Shared.DTO;

namespace Project_AMN.Client.Services
{
    public class InboundService
    {
        private readonly HttpClient _http;

        public InboundService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ArticleResultDto?> RegisterInboundAsync(string sku, int quantity)
        {
            // Skicka PUT request till inbound endpoint
            var response = await _http.PutAsJsonAsync($"/api/inbound/{sku}?quantity={quantity}", 
                                                       value: (object?)null);

            if (response.IsSuccessStatusCode)
            {
                var article = await response.Content.ReadFromJsonAsync<ArticleResultDto>();
                return article;
            }

            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Fel vid registrering: {error}");
            return null;
        }
    }
}
