using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Commands.Order.Create;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Services
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;
        public async Task<int> CreateOrderAsync(
    string userId,
    List<CreateOrderDetailCommand> orderDetails,
    CancellationToken cancellationToken = default)
        {
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
                    throw new Exception($"ProductVariant {item.ProductVariantId} not found.");

                var price = variant.Price * item.Quantity;

                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    Price = price
                });

                totalPrice += price;
            }

            order.TotalPrice = totalPrice;

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return 1;
        }
    }

}
