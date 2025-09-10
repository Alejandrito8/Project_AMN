
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

    public async Task<OrderResultDto?> UpdateOrderStatusAsync(OrderUpdateStatusDto dto)
    {
        var order = await _context.Orders.FindAsync(dto.OrderId);
        if (order == null) return null;

        order.Status = dto.Status;
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

    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
