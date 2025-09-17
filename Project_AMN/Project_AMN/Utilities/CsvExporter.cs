namespace Utilities
{
    public static class CsvExporter
    {
        public static byte[] ExportToCsv(List<string[]> rows, string delimiter = ",")
        {
            var sb = new StringBuilder();

            foreach (var row in rows)
            {
                var escaped = row.Select(EscapeField);
                sb.AppendLine(string.Join(delimiter, escaped));
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private static string EscapeField(string field)
        {
            if (field.Contains("\""))
                field = field.Replace("\"", "\"\"");

            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
                field = $"\"{field}\"";

            return field;
        }
    }
}
