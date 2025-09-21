using System.Net.Http.Json;
using Project_AMN.Shared.DTO;
using Project_AMN.Shared.Models;
namespace Project_AMN.Client.Services;

public class ArticleService
{
    private readonly HttpClient _http;

    public ArticleService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<ArticleResultDto?>> GetAllArticlesAsync()
    {
        var articles = await _http.GetFromJsonAsync<List<ArticleResultDto?>>("/api/articles");
        return articles ?? new List<ArticleResultDto?>();
    }
    // public async Task<ArticleResultDto?> GetArticleByIdAsync(int id)
    // {
    //     var article = await _http.GetFromJsonAsync<ArticleResultDto?>($"/api/articles/{id}");
    //     return article;
    // }

    public async Task<ArticleResultDto?> CreateArticleAsync(ArticleCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync("/api/articles", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ArticleResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Fel vid skapande: {error}");
        return null;
    }
    public async Task<ArticleResultDto?> UpdateArticleAsync(ArticleUpdateDto dto, string sku)
    {
        var response = await _http.PutAsJsonAsync($"/api/articles/{sku}", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ArticleResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Fel vid uppdatering: {error}");
        return null;
    }
    public async Task<bool> DeleteArticleAsync(int id)
    {
        var response = await _http.DeleteAsync($"/api/articles/{id}");
        return response.IsSuccessStatusCode;
    }
    public async Task<List<ArticleResultDto?>> SearchArticlesAsync(QueryFilter filter)
    {
        var response = await _http.PostAsJsonAsync("/api/articles/search", filter);

        if (response.IsSuccessStatusCode)
        {
            var articles = await response.Content.ReadFromJsonAsync<List<ArticleResultDto?>>();
            return articles ?? new List<ArticleResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Fel vid sökning: {error}");
        return new List<ArticleResultDto?>();
    }
    public async Task<byte[]> ExportArticlesAsync()
    {
        var response = await _http.GetAsync("/articles/export");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsByteArrayAsync();

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error whith export: {error}");
        return Array.Empty<byte>();
    }

}