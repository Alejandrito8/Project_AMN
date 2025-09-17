namespace Project_AMN.Services
{
    public static class ExportService
    {
        public static byte[] ExportOrders(List<Order> orders)
        {
            var rows = new List<string[]>();
            rows.Add(new[] { "OrderId", "Status", "CreatedAt", "TotalAmount" });

            foreach (var o in orders)
            {
                rows.Add(new[]
                {
                    o.OrderId.ToString(),
                    o.Status,
                    o.CreatedAt.ToString("yyyy-MM-dd"),
                    o.TotalAmount.ToString("F2")
                });
            }

            return CsvExporter.ExportToCsv(rows);
        }

        public static byte[] ExportArticles(List<Article> articles)
        {
            var rows = new List<string[]>();
            rows.Add(new[] { "ArticleId", "Name", "SKU", "Stock", "Location" });

            foreach (var a in articles)
            {
                rows.Add(new[]
                {
                    a.Id.ToString(),
                    a.Name,
                    a.SKU,
                    a.Stock.ToString(),
                    a.Location
                });
            }

            return CsvExporter.ExportToCsv(rows);
        }
    }
}
