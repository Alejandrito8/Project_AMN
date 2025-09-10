namespace Project_AMN.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderResultDto?>> GetAllOrdersAsync();
    Task<OrderResultDto?> GetOrderByIdAsync(int id);
    Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto);
    Task<OrderResultDto?> UpdateOrderStatusAsync(OrderUpdateStatusDto dto);
    Task DeleteOrderAsync(int id);
}