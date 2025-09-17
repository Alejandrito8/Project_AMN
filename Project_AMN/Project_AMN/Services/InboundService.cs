namespace Project_AMN.Services
{
    /// <summary>
    /// Service class to handle inbounds of articles.
    /// </summary>
    public class InboundService : IInboundService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of InboundService with the database context.        
        /// </summary>
        /// <param name="context">Entity Frameworks database context.</param>
        public InboundService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Register a inbound of an article.
        /// </summary>
        /// <param name="sku">Articel SKU.</param>
        /// <param name="quantity">Amount of Inbounds.</param>
        public async Task<bool> RegisterInboundAsync(string sku, int quantity)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.SKU == sku);
            if (article == null) return false;

            article.Stock += quantity;
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

    
