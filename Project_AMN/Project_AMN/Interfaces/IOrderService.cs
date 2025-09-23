namespace Project_AMN.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResultDto?>> GetAllOrdersAsync();
        Task<OrderResultDto?> GetOrderByIdAsync(int id);
        Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto);
        Task<OrderResultDto?> UpdateOrderStatusAsync(int orderId);
        Task<bool> DeleteOrderAsync(int orderId);
        Task<OrderItemResultDto?> AddItemToOrderAsync(int orderId, int articleId, int quantity, decimal orderPrice);
        Task<IEnumerable<OrderItemResultDto>> GetOrderItemsAsync(int orderId);
        Task<IEnumerable<OrderResultDto>> SearchOrdersAsync(string? status, DateTime? fromDate, DateTime? toDate);
    }
}
