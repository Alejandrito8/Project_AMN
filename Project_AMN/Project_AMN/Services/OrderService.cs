namespace Project_AMN.Services
{
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
                Status = "Created",
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
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return null;

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
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

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
                Id = orderItem.Id,
                ArticleId = articleId,
                ArticleName = article.Name,
                Quantity = quantity,
                OrderPrice = orderPrice
            };
        }

        public async Task<IEnumerable<OrderItemResultDto>> GetOrderItemsAsync(int orderId)
        {
            return await _context.OrderItems
                .Include(oi => oi.Article)
                .Where(oi => oi.OrderId == orderId)
                .Select(oi => new OrderItemResultDto
                {
                    Id = oi.Id,
                    ArticleId = oi.ArticleId,
                    ArticleName = oi.Article.Name,
                    Quantity = oi.Quantity,
                    OrderPrice = oi.OrderPrice
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderResultDto>> SearchOrdersAsync(string? status, DateTime? fromDate, DateTime? toDate)
        {
            var query = _context.Orders.AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(o => o.Status.ToLower().Contains(status.Trim().ToLower()));

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
                    TrackingNumber = o.TrackingNumber
                })
                .ToListAsync();
        }
    }
}
