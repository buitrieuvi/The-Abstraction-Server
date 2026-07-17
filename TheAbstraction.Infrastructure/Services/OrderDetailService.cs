using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Services
{
    public class OrderDetailService(ApplicationDbContext context) : IOrderDetailService
    {
        private readonly ApplicationDbContext _context = context;

        public Task<int> CreateOrderDetailAsync(string orderId, string productVariantId, int quantity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }

}
