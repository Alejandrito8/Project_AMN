namespace Project_AMN.Interfaces;

/// <summary>
/// Defines service methods for managing articles.
/// Provides CRUD operations (Create, Read, Update, Delete) using DTOs.
/// </summary>
public interface IArticleService
{
    /// <summary>
    /// Retrieves all articles.
    /// </summary>
    Task<IEnumerable<ArticleResultDto?>> GetAllArticlesAsync();

    /// <summary>
    /// Retrieves a specific article by its ID.
    /// </summary>
    Task<ArticleResultDto?> GetArticleByIdAsync(int id);

    /// <summary>
    /// Adds a new article.
    /// Returns the created article as a DTO.
    /// </summary>
    Task<ArticleResultDto?> CreateArticleAsync(ArticleCreateDto dto);

    /// <summary>
    /// Updates an existing article based on SKU.
    /// Returns the updated article DTO, or null if not found.
    /// </summary>
    Task<ArticleResultDto?> UpdateArticleAsync(ArticleUpdateDto dto, string sku);

    /// <summary>
    /// Deletes an article by ID.
    /// Returns true if deleted, false if not found.
    /// </summary>
    Task<bool> DeleteArticleAsync(int id);
   
}