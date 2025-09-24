using System.Net.Http.Json;
using Project_AMN.Shared.DTO;
using Project_AMN.Shared.Models;

namespace Project_AMN.Client.ApiRoutes;

/// <summary>
/// Provides client-side methods for managing articles via HTTP API.
/// </summary>
public class ArticleRoute
{
    private readonly HttpClient _http;

    /// <summary>
    /// Initializes a new instance of the <see cref="ArticleRoute"/> class.
    /// </summary>
    public ArticleRoute(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Retrieves all articles from the API.
    /// </summary>
    public async Task<List<ArticleResultDto?>> GetAllArticlesAsync()
    {
        var articles = await _http.GetFromJsonAsync<List<ArticleResultDto?>>("/api/articles");
        return articles ?? new List<ArticleResultDto?>();
    }

    // /// <summary>
    // /// Retrieves an article by its ID.
    // /// </summary>
    // public async Task<ArticleResultDto?> GetArticleByIdAsync(int id)
    // {
    //     var article = await _http.GetFromJsonAsync<ArticleResultDto?>($"/api/articles/{id}");
    //     return article;
    // }

    /// <summary>
    /// Creates a new article via the API.
    /// </summary>
    public async Task<ArticleResultDto?> CreateArticleAsync(ArticleCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync("/api/articles", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ArticleResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while creating article: {error}");
        return null;
    }

    /// <summary>
    /// Updates an existing article via the API using SKU.
    /// </summary>
    public async Task<ArticleResultDto?> UpdateArticleAsync(ArticleUpdateDto dto, string sku)
    {
        var response = await _http.PutAsJsonAsync($"/api/articles/{sku}", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ArticleResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while updating article: {error}");
        return null;
    }

    /// <summary>
    /// Deletes an article by ID via the API.
    /// </summary>
    public async Task<bool> DeleteArticleAsync(int id)
    {
        var response = await _http.DeleteAsync($"/api/articles/{id}");
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Searches for articles using a filter object.
    /// </summary>
    public async Task<List<ArticleResultDto?>> SearchArticlesAsync(QueryFilter filter)
    {
        var response = await _http.PostAsJsonAsync("/api/articles/search", filter);

        if (response.IsSuccessStatusCode)
        {
            var articles = await response.Content.ReadFromJsonAsync<List<ArticleResultDto?>>();
            return articles ?? new List<ArticleResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while searching articles: {error}");
        return new List<ArticleResultDto?>();
    }

    /// <summary>
    /// Exports all articles as a CSV file and returns it as a byte array.
    /// </summary>
    public async Task<byte[]> ExportArticlesAsync()
    {
        var response = await _http.GetAsync("/articles/export");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsByteArrayAsync();

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with export: {error}");
        return Array.Empty<byte>();
    }
}
