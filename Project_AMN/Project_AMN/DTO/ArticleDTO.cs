namespace Project_AMN.DTO
{
    /// <summary>
    /// Data Transfer Object (DTO) for creating a new article.
    /// Used when the client sends data to create a new article.
    /// </summary>
    public class ArticleCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Stock { get; set; }
         public decimal Price { get; set; } 
        public string Location { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object (DTO) for updating an article.
    /// Used when the client modifies existing article data.
    /// </summary>
    public class ArticleUpdateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }

    /// <summary>
    /// Data Transfer Object (DTO) for returning article data to the client.
    /// Used when sending results back to the user.
    /// </summary>
    public class ArticleResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SKU { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; } 
        public string Location { get; set; } = string.Empty;
    }
}
