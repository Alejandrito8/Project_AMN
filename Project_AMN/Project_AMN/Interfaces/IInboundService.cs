namespace Project_AMN.Interfaces;

/// <summary>
/// Defines service methods for managing articles.
/// Provides CRUD operations (Create, Read, Update, Delete).
/// </summary>
public interface IInboundService
{
    /// <summary>
    /// Retrieves all articles from the database.
    /// </summary>
    Task<IEnumerable<Article>> GetAllArticlesAsync();

    /// <summary>
    /// Retrieves a specific article by its ID.
    /// </summary>
    Task<Article> GetArticleByIdAsync(int id);

    /// <summary>
    /// Adds a new article to the database.
    /// </summary>
    Task AddArticleAsync(Article article);

    /// <summary>
    /// Updates an existing article in the database.
    /// </summary>
    Task UpdateArticleAsync(Article article);

    /// <summary>
    /// Deletes an article from the database by its ID.
    /// </summary>
    Task<bool> DeleteArticleAsync(int id);
}  




