namespace Project_AMN.Interfaces;

/// <summary>
/// Defines operations for managing orders and their items.
/// </summary>
public interface IOrderService
{
    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    Task<IEnumerable<OrderResultDto?>> GetAllOrdersAsync();

    /// <summary>
    /// Retrieves a single order by ID.
    /// </summary>
    Task<OrderResultDto?> GetOrderByIdAsync(int id);

    /// <summary>
    /// Creates a new order.
    /// </summary>
    Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto);

    /// <summary>
    /// Updates the status of an existing order.
    /// </summary>
    Task<OrderResultDto?> UpdateOrderStatusAsync(int orderId);

    /// <summary>
    /// Deletes an existing order by ID.
    /// </summary>
    Task<bool> DeleteOrderAsync(int orderId);

    /// <summary>
    /// Adds a new item to an existing order.
    /// </summary>
    Task<OrderItemResultDto?> AddItemToOrderAsync(int orderId, int articleId, int quantity, decimal orderPrice);

    /// <summary>
    /// Retrieves all items of a specific order.
    /// </summary>
    Task<IEnumerable<OrderItemResultDto>> GetOrderItemsAsync(int orderId);

    /// <summary>
    /// Searches for orders based on status and date range.
    /// </summary>
    Task<IEnumerable<OrderResultDto>> SearchOrdersAsync(string? status, DateTime? fromDate, DateTime? toDate);
}
