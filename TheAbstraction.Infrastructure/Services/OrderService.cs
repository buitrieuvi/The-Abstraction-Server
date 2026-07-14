using TheAbstraction.Application.Commands.Order.Create;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infra.Data;

namespace TheAbstraction.Infra.Services
{
    public class OrderService(ApplicationDbContext context) : IOrderService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<int> CreateOrderAsync(CreateOrderCommand orderCommand, CancellationToken cancellationToken = default)
        {
            decimal totalPrice = 0;

            Order order = new()
            {
                UserId = orderCommand.UserId
            };

            foreach (var orderDetail in orderCommand.OrderDetails)
            {
                var productVariant = await _context.ProductVariants.FindAsync(new ProductVariant { Id = orderDetail.ProductVariantId }, cancellationToken);
                decimal price = productVariant.Price * orderDetail.Quantity;
                totalPrice += price;

                _context.OrderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductVariantId = orderDetail.ProductVariantId,
                    Quantity = orderDetail.Quantity,
                    Price = price
                });
            }

            order.TotalPrice = totalPrice;

            _context.Orders.Add(order);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }

}
