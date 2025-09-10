namespace Project_AMN.Services
{
    /// <summary>
    /// Service class to handle incoming operations related to articles.
    /// Implements CRUD functionality (Create, Read, Update, Delete).
    /// </summary>
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;
 
        /// <summary>
        /// Initializes a new instance of Articleservice with the database context.        
        /// </summary>
        /// <param name="context">Entity Frameworks database context.</param>
        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }
 
        /// <summary>
        /// Adds a new article to the database.
        /// </summary>
        /// <param name="article">The article entity to add.</param>
        public async Task AddArticleAsync(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
        }
 
        /// <summary>
        /// Removes an article from the database based on its ID.
        /// </summary>
        /// <param name="id">ID of the article to delete.</param>
        public async Task DeleteArticleAsync(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
 
        /// <summary>
        /// Retrieves all articles from the database.
        /// </summary>
        /// <returns>A list of all articles.</returns>
        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Articles.ToListAsync();
        }
 
        /// <summary>
        /// Retrieves an article from the database based on its ID.
        /// </summary>
        /// <param name="id">ID of the article to retrieve.</param>
        /// <returns>The article if found, otherwise null.</returns>
        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }
 
        /// <summary>
        /// Updates an existing article in the database.
        /// </summary>
        /// <param name="article">The updated article entity.</param>
        public async Task UpdateArticleAsync(Article article)
        {
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }
    }
}