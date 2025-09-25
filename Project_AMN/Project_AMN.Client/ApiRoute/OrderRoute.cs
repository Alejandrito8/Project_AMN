using Project_AMN.Shared.DTO;
using System.Net.Http.Json;

namespace Project_AMN.Client.ApiRoutes;

/// <summary>
/// Provides client-side methods for managing orders via the API.
/// </summary>
public class OrderRoute
{
    private readonly HttpClient _http;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderRoute"/> class.
    /// </summary>
    public OrderRoute(HttpClient http)
    {
        _http = http;
    }

    /// <summary>
    /// Retrieves all orders from the API.
    /// </summary>
    public async Task<List<OrderResultDto?>> GetAllOrdersAsync()
    {
        var orders = await _http.GetFromJsonAsync<List<OrderResultDto?>>("http://localhost:5000/api/orders");
        return orders ?? new List<OrderResultDto?>();
    }

    /// <summary>
    /// Creates a new order via the API.
    /// </summary>
    public async Task<OrderResultDto?> CreateOrderAsync(OrderCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync("http://localhost:5000/api/orders", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<OrderResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while creating order: {error}");
        return null;
    }

    /// <summary>
    /// Updates the status of an existing order via the API.
    /// </summary>
    public async Task<OrderResultDto?> UpdateOrderStatusAsync(OrderUpdateStatusDto dto)
    {
        var response = await _http.PutAsJsonAsync($"http://localhost:5000/api/orders/{dto.OrderId}/status", dto);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<OrderResultDto?>();
        }
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while updating order status: {error}");
        return null;
    }

    /// <summary>
    /// Deletes an order by ID via the API.
    /// </summary>
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/orders/{id}");
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Searches for orders using a filter object.
    /// </summary>
    public async Task<List<OrderResultDto?>> SearchOrdersAsync(OrderSearchRequest filter)
    {
        var query = $"?status={filter.Status}&fromDate={filter.FromDate:yyyy-MM-dd}&toDate={filter.ToDate:yyyy-MM-dd}";
        var response = await _http.GetAsync($"/api/orders/search{query}");
        if (response.IsSuccessStatusCode)
        {
            var orders = await response.Content.ReadFromJsonAsync<List<OrderResultDto?>>();
            return orders ?? new List<OrderResultDto?>();
        }
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while searching orders: {error}");
        return new List<OrderResultDto?>();
    }

    /// <summary>
    /// Exports all orders as a CSV file and returns it as a byte array.
    /// </summary>
    public async Task<byte[]> ExportOrdersAsync()
    {
        var response = await _http.GetAsync("orders/export");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsByteArrayAsync();

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error while exporting orders: {error}");
        return Array.Empty<byte>();
    }
}