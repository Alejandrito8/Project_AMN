namespace Project_AMN.Services;

/// <summary>
/// Provides services for managing orders and order items.
/// </summary>
public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderService"/> class.
    /// </summary>
    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves all orders.
    /// </summary>
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
                TrackingNumber = o.TrackingNumber,
                Items = o.Items.Select(i => new OrderItemResultDto
                {
                    ArticleId = i.ArticleId,
                    ArticleName = i.Article.Name,
                    Quantity = i.Quantity,
                    OrderPrice = i.OrderPrice
                }).ToList()
            })
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a single order by ID.
    /// </summary>
    public async Task<OrderResultDto?> GetOrderByIdAsync(int id)
    {
        var order = await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.Article)
            .FirstOrDefaultAsync(o => o.OrderId == id);

        if (order == null) return null;

        return new OrderResultDto
        {
            OrderId = order.OrderId,
            CreatedAt = order.CreatedAt,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            ShippingAddress = order.ShippingAddress,
            TrackingNumber = order.TrackingNumber,
            Items = order.Items.Select(i => new OrderItemResultDto
            {
                ArticleId = i.ArticleId,
                ArticleName = i.Article.Name,
                Quantity = i.Quantity,
                OrderPrice = i.OrderPrice
            }).ToList()
        };
    }

    /// <summary>
    /// Creates a new order.
    /// </summary>
    public async Task<OrderResultDto> CreateOrderAsync(OrderCreateDto dto)
    {
        if (dto.Items == null || !dto.Items.Any())
            throw new ArgumentException("Order must contain at least one item.", nameof(dto.Items));

        var order = new Order
        {
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Created,
            TotalAmount = dto.TotalAmount,
            ShippingAddress = dto.ShippingAddress
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        foreach (var itemDto in dto.Items)
        {
            var article = await _context.Articles.FindAsync(itemDto.ArticleId);
            if (article == null)
                throw new InvalidOperationException($"Article with ID {itemDto.ArticleId} not found.");

            if (article.Stock < itemDto.Quantity)
                throw new InvalidOperationException($"Not enough stock for article '{article.Name}'.");

            article.Stock -= itemDto.Quantity;

            var orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                ArticleId = itemDto.ArticleId,
                Quantity = itemDto.Quantity,
                OrderPrice = itemDto.OrderPrice
            };

            _context.OrderItems.Add(orderItem);
        }

        await _context.SaveChangesAsync();

        return await GetOrderByIdAsync(order.OrderId);
    }


    /// <summary>
    /// Updates the status of an existing order.
    /// </summary>
    public async Task<OrderResultDto?> UpdateOrderStatusAsync(OrderUpdateStatusDto dto)
    {
        var order = await _context.Orders.FindAsync(dto.OrderId);
        if (order == null) return null;

        order.Status = dto.Status;
        await _context.SaveChangesAsync();

        return await GetOrderByIdAsync(order.OrderId);
    }

    /// <summary>
    /// Deletes an existing order by ID.
    /// </summary>
    public async Task<bool> DeleteOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        if (order == null) return false;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Adds a new item to an existing order.
    /// </summary>
    public async Task<OrderItemResultDto?> AddItemToOrderAsync(int orderId, int articleId, int quantity, decimal orderPrice)
    {
        var order = await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null) return null;

        var article = await _context.Articles.FindAsync(articleId);
        if (article == null) return null;

        var orderItem = new OrderItem
        {
            OrderId = orderId,
            ArticleId = articleId,
            Quantity = quantity,
            OrderPrice = orderPrice
        };

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();

        return new OrderItemResultDto
        {
            ArticleId = articleId,
            ArticleName = article.Name,
            Quantity = quantity,
            OrderPrice = orderPrice
        };
    }

    /// <summary>
    /// Retrieves all items of a specific order.
    /// </summary>
    public async Task<IEnumerable<OrderItemResultDto>> GetOrderItemsAsync(int orderId)
    {
        return await _context.OrderItems
            .Include(oi => oi.Article)
            .Where(oi => oi.OrderId == orderId)
            .Select(oi => new OrderItemResultDto
            {
                ArticleId = oi.ArticleId,
                ArticleName = oi.Article.Name,
                Quantity = oi.Quantity,
                OrderPrice = oi.OrderPrice
            })
            .ToListAsync();
    }

    /// <summary>
    /// Searches for orders based on status and date range.
    /// </summary>
    public async Task<IEnumerable<OrderResultDto>> SearchOrdersAsync(OrderStatus? status, DateTime? fromDate, DateTime? toDate)
    {
        var query = _context.Orders.Include(o => o.Items).ThenInclude(i => i.Article).AsQueryable();

        if (status.HasValue)
            query = query.Where(o => o.Status == status.Value);

        if (fromDate.HasValue)
            query = query.Where(o => o.CreatedAt >= fromDate.Value);

        if (toDate.HasValue)
            query = query.Where(o => o.CreatedAt <= toDate.Value);

        return await query
            .Select(o => new OrderResultDto
            {
                OrderId = o.OrderId,
                CreatedAt = o.CreatedAt,
                Status = o.Status,
                TotalAmount = o.TotalAmount,
                ShippingAddress = o.ShippingAddress,
                TrackingNumber = o.TrackingNumber,
                Items = o.Items.Select(i => new OrderItemResultDto
                {
                    ArticleId = i.ArticleId,
                    ArticleName = i.Article.Name,
                    Quantity = i.Quantity,
                    OrderPrice = i.OrderPrice
                }).ToList()
            })
            .ToListAsync();
    }
}