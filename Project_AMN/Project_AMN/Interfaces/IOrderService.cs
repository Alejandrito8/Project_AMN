using System.Collections.Generic;
using System.Threading.Tasks;
using Project_AMN.Models;

namespace Project_AMN.Interfaces;

public interface IOrderService
{
    // Define methods for handling orders, e.g., creating orders, updating status, retrieving order details, etc.
    Task<IEnumerable<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int id);
    Task CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
}