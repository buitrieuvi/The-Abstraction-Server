using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infra.Data;

namespace TheAbstraction.Infra.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly ApplicationDbContext _context;

        public ProductVariantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string productId, decimal price, int quantity, CancellationToken cancellationToken = default)
        {
            _context.ProductVariants.Add(new ProductVariant
            {
                ProductId = productId,
                Price = price,
                Quantity = quantity
            });
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}