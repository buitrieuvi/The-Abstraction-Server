using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Services
{
    public class OrderDetailService(ApplicationDbContext context) : IOrderDetailService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<int> CreateOrderDetailAsync(string orderId, string productVariantId, int quantity, CancellationToken cancellationToken = default)
        {
            if (quantity <= 0)
            {
                throw new BadRequestException("Số lượng phải lớn hơn 0");
            }

            var order = await _context.Orders.FindAsync([orderId], cancellationToken)
                ?? throw new NotFoundException("Không tìm thấy đơn hàng");
            var variant = await _context.ProductVariants.FindAsync([productVariantId], cancellationToken)
                ?? throw new NotFoundException("Không tìm thấy mẫu mã sản phẩm");

            if (variant.Quantity < quantity)
            {
                throw new BadRequestException("Số lượng tồn kho không đủ");
            }

            variant.Quantity -= quantity;
            _context.OrderDetails.Add(new OrderDetail
            {
                OrderId = order.Id,
                ProductVariantId = variant.Id,
                Quantity = quantity,
                Price = variant.Price * quantity
            });
            order.TotalPrice += variant.Price * quantity;

            await _context.SaveChangesAsync(cancellationToken);
            return 1;
        }
    }

}
