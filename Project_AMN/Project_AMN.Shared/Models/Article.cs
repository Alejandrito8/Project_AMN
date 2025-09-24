namespace Project_AMN.Shared.Models
{
    public class Article
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the name of the article.
        /// </summary>
        public string Name { get; set; }
        public string SKU { get; set; } = string.Empty;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; } = string.Empty;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}