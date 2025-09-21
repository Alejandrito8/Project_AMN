using Project_AMN.Shared.DTO;
using System.Net.Http.Json;
namespace Project_AMN.Client.Services;

public class OrderService
{
    private readonly HttpClient _http;

    public OrderService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<OrderResultDto?>> GetAllOrdersAsync()
    {
        var orders = await _http.GetFromJsonAsync<List<OrderResultDto?>>("/api/orders");
        return orders ?? new List<OrderResultDto?>();
    }
    // public async Task<OrderResultDto?> GetOrderByIdAsync(int id)
    // {
    //     var order = await _http.GetFromJsonAsync<OrderResultDto?>($"/api/orders/{id}");
    //     return order;
    // }

    public async Task<OrderResultDto?> CreateOrderAsync(OrderCreateDto dto)
    {
        var response = await _http.PostAsJsonAsync("/api/orders", dto);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<OrderResultDto?>();
        }

        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Fel vid skapande: {error}");
        return null;
    }
    public async Task<OrderResultDto?> UpdateOrderStatusAsync(OrderUpdateStatusDto dto)
    {
        var response = await _http.PutAsJsonAsync($"/api/orders/{dto.OrderId}/status", dto);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<OrderResultDto?>();
        }
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Error with update of status: {error}");
        return null;
    }
    public async Task<bool> DeleteOrderAsync(int id)
    {
        var response = await _http.DeleteAsync($"/api/orders/{id}");
        return response.IsSuccessStatusCode;
    }
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
        Console.WriteLine($"Error with search: {error}");
        return new List<OrderResultDto?>();
    }
    public async Task<byte[]> ExportOrdersAsync()
    {
        var response = await _http.GetAsync("/orders/export");
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsByteArrayAsync();
        var error = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Fel vid export: {error}");
        return Array.Empty<byte>();
    }

}