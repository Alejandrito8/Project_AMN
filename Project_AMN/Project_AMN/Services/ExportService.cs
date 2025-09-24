namespace Project_AMN.Services;

/// <summary>
/// Provides functionality to export data to CSV format.
/// </summary>
public static class ExportService
{
    /// <summary>
    /// Exports orders to a CSV file.
    /// </summary>
public static byte[] ExportOrders(List<Order> orders)
{
    var rows = new List<string[]>();

    // Header
    rows.Add(new[]
    {
        "OrderId",
        "CreatedAt",
        "Status",
        "TotalAmount",
        "ShippingAddress",
        "TrackingNumber",
        "ArticleId",
        "ArticleName",
        "Quantity",
        "OrderPrice"
    });

    foreach (var o in orders)
    {
        if (o.Items == null || !o.Items.Any())
        {
            rows.Add(new[]
            {
                o.OrderId.ToString(),
                o.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                o.Status.ToString(),
                o.TotalAmount.ToString("F2"),
                o.ShippingAddress,
                o.TrackingNumber ?? string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty
            });
        }
        else
        {
            foreach (var item in o.Items)
            {
                rows.Add(new[]
                {
                    o.OrderId.ToString(),
                    o.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    o.Status.ToString(),
                    o.TotalAmount.ToString("F2"),
                    o.ShippingAddress,
                    o.TrackingNumber ?? string.Empty,
                    item.ArticleId.ToString(),
                    item.Article?.Name ?? string.Empty, // <- Här hämtas ArticleName från navigeringsobjektet
                    item.Quantity.ToString(),
                    item.OrderPrice.ToString("F2")
                });
            }
        }
    }

    return CsvExporter.ExportToCsv(rows);
}


    /// <summary>
    /// Exports articles to a CSV file.
    /// </summary>
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
