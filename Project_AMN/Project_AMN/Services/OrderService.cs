using Project_AMN.Interfaces;
using Project_AMN.Models;

public class OrderService : IOrderService
{
    // Implementation of IInboundService methods
    public Task CreateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public Task DeleteOrderAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }
}