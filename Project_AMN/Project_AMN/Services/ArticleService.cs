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
        public async Task<ArticleResultDto?> AddArticleAsync(ArticleCreateDto dto)
        {
            var article = new Article
            {
                Name = dto.Name,
                SKU = dto.SKU,
                Stock = dto.Stock,
                Location = dto.Location
            };
 
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
           
            return new ArticleResultDto
            {
                Id = article.Id,
                Name = article.Name,
                SKU = article.SKU,
                Stock = article.Stock,
                Location = article.Location
            };
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
        public async Task<IEnumerable<ArticleResultDto?>> GetAllArticlesAsync()
        {
            return await _context.Articles
            .Select(a => new ArticleResultDto
            {
                Id = a.Id,
                Name = a.Name,
                SKU = a.SKU,
                Stock = a.Stock,
                Location = a.Location
            })
            .ToListAsync();
        }
 
        /// <summary>
        /// Retrieves an article from the database based on its ID.
        /// </summary>
        /// <param name="id">ID of the article to retrieve.</param>
        /// <returns>The article if found, otherwise null.</returns>
        public async Task<ArticleResultDto?> GetArticleByIdAsync(int id)
        {
            return await _context.Articles
            .Where(a => a.Id == id)
            .Select(a => new ArticleResultDto
            {
                Id = a.Id,
                Name = a.Name,
                SKU = a.SKU,
                Stock = a.Stock,
                Location = a.Location
            })
            .FirstOrDefaultAsync();
        }
 
        /// <summary>
        /// Updates an existing article in the database.
        /// </summary>
        /// <param name="article">The updated article entity.</param>
        public async Task<ArticleResultDto?> UpdateArticleAsync(ArticleUpdateDto dto)
        {
            var article = await _context.Articles.FindAsync(dto.Id);
            if (article == null) return null;
            // article.Name = dto.Name; ska vi kunna ändra namn på artikeln?
            article.Stock = dto.Stock;
            article.Location = dto.Location;
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
            return new ArticleResultDto
            {
                Id = article.Id,
                Name = article.Name,
                SKU = article.SKU,
                Stock = article.Stock,
                Location = article.Location
            };
        }
    }
}