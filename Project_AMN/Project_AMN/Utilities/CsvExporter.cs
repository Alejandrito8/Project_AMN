namespace Utilities;

/// <summary>
/// Provides functionality to export data to CSV format.
/// </summary>
public static class CsvExporter
{
    /// <summary>
    /// Converts a list of string arrays into a CSV byte array.
    /// </summary>
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

    /// <summary>
    /// Escapes a CSV field to ensure correct formatting, handling quotes, commas, and newlines.
    /// </summary>
    private static string EscapeField(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            field = $"\"{field}\"";

        return field;
    }
}
