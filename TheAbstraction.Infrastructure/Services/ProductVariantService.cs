using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
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

        public async Task<int> CreateProductVariantAsync(string productId, decimal price, int quantity, string model, string color, string size, CancellationToken cancellationToken = default)
        {
            _context.ProductVariants.Add(new ProductVariant
            {
                ProductId = productId,
                Price = price,
                Quantity = quantity,
                Model = model,
                Color = color,
                Size = size
            });
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteProductVariantAsync(string id, CancellationToken cancellationToken = default)
        {
            var productVariant = await _context.ProductVariants.FirstOrDefaultAsync(pv => pv.Id == id, cancellationToken);

            if (productVariant != null)
            {
                _context.ProductVariants.Remove(productVariant);
            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<ProductVariantResponseDTO>> GetProductVariantAsync(CancellationToken cancellationToken = default)
        {
            var productVariants = await _context.ProductVariants.ToListAsync(cancellationToken);

            return productVariants.Select(pv => new ProductVariantResponseDTO
            {
                Id = pv.Id,
                ProductId = pv.ProductId,
                Price = pv.Price,
                Quantity = pv.Quantity,
                Model = pv.Model,
                Color = pv.Color,
                Size = pv.Size
            }).ToList();
        }

        public async Task<IReadOnlyList<ProductVariantResponseDTO>> GetProductVariantByProductIdAsync(string productId, CancellationToken cancellationToken = default)
        {
            var productVariants = await _context.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .ToListAsync(cancellationToken); 

            return productVariants.Select(pv => new ProductVariantResponseDTO
            {
                Id = pv.Id,
                ProductId = pv.ProductId,
                Price = pv.Price,
                Quantity = pv.Quantity,
                Model = pv.Model,
                Color = pv.Color,
                Size = pv.Size
            }).ToList();

        }
    }
}