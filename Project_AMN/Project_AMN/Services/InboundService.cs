namespace Project_AMN.Services;

/// <summary>
///  
/// </summary>
public class InboundService : IInboundService
{
    // Constants
    private const string PlaceHolder = null;
    private const string PlaceHolder1 = null;

    private const int PlaceHolder2 = 2;

    private readonly ApplicationDbContext _context;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="article"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task AddArticleAsync(Article article)
    {
        throw new NotImplementedException();
    }

    public Task DeleteArticleAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Article>> GetAllArticlesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Article> GetArticleByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateArticleAsync(Article article)
    {
        throw new NotImplementedException();
    }
}  