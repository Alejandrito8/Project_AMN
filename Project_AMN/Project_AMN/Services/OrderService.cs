
namespace Project_AMN.Services;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderResultDto?>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Select(o => new OrderResultDto
            {
                OrderId = o.OrderId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                ShippingAddress = o.ShippingAddress,
                TrackingNumber = o.TrackingNumber
            })
            .ToListAsync();
    }

    public async Task<OrderResultDto?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Where(o => o.OrderId == id)
            .Select(o => new OrderResultDto
            {
                OrderId = o.OrderId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                ShippingAddress = o.ShippingAddress,
                TrackingNumber = o.TrackingNumber
            })
            .FirstOrDefaultAsync();
    }

    public async Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto)
    {
        var order = new Order
        {
            CreatedAt = DateTime.UtcNow,
            Status = "Created",  //OM VI ORKAR LÄGGER VI TILL EN TIMER SOM ÄNDRA STATUS EFTER ETT TAG
            TotalAmount = dto.TotalAmount,
            ShippingAddress = dto.ShippingAddress
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return new OrderResultDto
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            ShippingAddress = order.ShippingAddress,
            TrackingNumber = order.TrackingNumber
        };
    }

    public async Task<OrderResultDto?> UpdateOrderStatusAsync(int orderId)
    {

        if (orderId == null) return null;

        var order = await _context.Orders.FindAsync(orderId);
        order.Status = "Sent";
        await _context.SaveChangesAsync();

        return new OrderResultDto
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            ShippingAddress = order.ShippingAddress,
            TrackingNumber = order.TrackingNumber
        };
    }

public async Task<bool> DeleteOrderAsync(int orderId)
{
    var order = await _context.Orders.FindAsync(orderId);

    if (order == null)
    {
        Console.WriteLine($"Hittade ingen order med ID {orderId}");
        return false;
    }

    Console.WriteLine($"Hittade order {order.OrderId}, raderar nu...");
    _context.Orders.Remove(order);
    await _context.SaveChangesAsync();
    return true;
}


}
