namespace Project_AMN.Shared.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;

        public int Quantity { get; set; }
        public decimal OrderPrice { get; set; }
    }
}
