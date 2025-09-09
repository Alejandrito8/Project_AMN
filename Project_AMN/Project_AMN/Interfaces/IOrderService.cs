namespace Project_AMN.Interfaces;

public interface IOrderService
{
    // Define methods for handling orders, e.g., creating orders, updating status, retrieving order details, etc.
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int id);
    Task CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);

    // --- Specifika flödessteg ---
    // Task<Order?> PlaceOrderAsync(Order order);
    // // Skapar en ny order med status "Created"

    // Task<bool> SendOrderAsync(int orderId);
    // // Ändrar status till "Sent" och kanske genererar trackingnummer

    // Task<bool> CompleteOrderAsync(int orderId);
    // // Ändrar status till "Delivered"

    // Task<bool> CancelOrderAsync(int orderId);
    // // Ändrar status till "Cancelled"

    // Task<IEnumerable<Order>> GetOrderHistoryByUserAsync(string userId);
    // // Hämtar alla ordrar som en viss användare har gjort

    // Utökad funktionalitet kan läggas till efter behov!
}