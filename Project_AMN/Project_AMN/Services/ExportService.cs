namespace Services
{
    public class ExportService
    {
        /// <summary>
        /// Exporterar orders till CSV
        /// </summary>
        public byte[] ExportOrders(List<Order> orders)
        {
            var rows = new List<string[]>();
            rows.Add(new[] { "Ordernummer", "Status", "Datum", "TotalAmount" });

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
    }
}
