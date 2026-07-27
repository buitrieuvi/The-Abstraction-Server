using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infrastructure.Data;
using TheAbstraction.Application.Commands.Order;

namespace TheAbstraction.Infrastructure.Services
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<int> CreateOrderAsync(string userId, List<CreateOrderDetailCommand> orderDetails, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ForbiddenAccessException("Bạn cần đăng nhập để tạo đơn hàng");
            }

            if (orderDetails == null || orderDetails.Count == 0 || orderDetails.Any(x => x.Quantity <= 0 || string.IsNullOrWhiteSpace(x.ProductVariantId)))
            {
                throw new BadRequestException("Đơn hàng phải có ít nhất một sản phẩm với số lượng hợp lệ");
            }

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

            var order = new Order
            {
                UserId = userId
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync(cancellationToken);

            var variantIds = orderDetails
                .Select(x => x.ProductVariantId)
                .Distinct()
                .ToList();

            var variants = await _context.ProductVariants
                .Where(x => variantIds.Contains(x.Id))
                .ToDictionaryAsync(x => x.Id, cancellationToken);

            decimal totalPrice = 0;

            foreach (var item in orderDetails)
            {
                if (!variants.TryGetValue(item.ProductVariantId, out var variant))
                    throw new NotFoundException($"Không tìm thấy mẫu mã sản phẩm {item.ProductVariantId}");

                if (variant.Quantity < item.Quantity)
                {
                    throw new BadRequestException("Sản phẩm không còn đủ tồn kho");
                }

                var price = variant.Price * item.Quantity;

                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    Price = price
                });

                totalPrice += price;
                variant.Quantity -= item.Quantity;
            }

            order.TotalPrice = totalPrice;

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return 1;
        }

        public async Task<int> UpdateOrderStatus(string orderId, string newStatus, CancellationToken cancellationToken = default)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

            if (order == null) { throw new NotFoundException("Không tìm thấy đơn hàng"); }

            order.OrderStatus = newStatus;
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
