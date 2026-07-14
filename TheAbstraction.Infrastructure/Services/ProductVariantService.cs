using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infra.Data;

namespace TheAbstraction.Infra.Services
{
    public class ProductVariantService(ApplicationDbContext context) : IProductVariantService
    {
        private readonly ApplicationDbContext _context = context;

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
                await _context.SaveChangesAsync(cancellationToken);

                int c = _context.ProductVariants.Where(p => p.ProductId == productVariant.ProductId).Count();
                if (c == 0)
                {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productVariant.ProductId, cancellationToken);
                    if (product != null)
                    {
                        _context.Products.Remove(product);
                    }
                }

            }
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<ProductVariantResponseDTO>> GetProductVariantAsync(CancellationToken cancellationToken = default)
        {
            var productVariants = await _context.ProductVariants.ToListAsync(cancellationToken);

            return [.. productVariants.Select(pv => new ProductVariantResponseDTO
            {
                Id = pv.Id,
                ProductId = pv.ProductId,
                Price = pv.Price,
                Quantity = pv.Quantity,
                Model = pv.Model,
                Color = pv.Color,
                Size = pv.Size
            })];
        }

        public async Task<IReadOnlyList<ProductVariantResponseDTO>> GetProductVariantByProductIdAsync(string productId, CancellationToken cancellationToken = default)
        {
            var productVariants = await _context.ProductVariants
                .Where(pv => pv.ProductId == productId)
                .ToListAsync(cancellationToken);

            return [.. productVariants.Select(pv => new ProductVariantResponseDTO
            {
                Id = pv.Id,
                ProductId = pv.ProductId,
                Price = pv.Price,
                Quantity = pv.Quantity,
                Model = pv.Model,
                Color = pv.Color,
                Size = pv.Size
            })];

        }

        public async Task<int> UpdateProductVariantAsync(string id, decimal price, int quantity, bool isActive,
         string model, string color, string size, CancellationToken cancellationToken = default)
        {
            var productVariant = await _context.ProductVariants.FirstOrDefaultAsync(pv => pv.Id == id, cancellationToken);

            if (productVariant != null)
            {
                productVariant.Price = price;
                productVariant.Quantity = quantity;
                productVariant.IsActive = isActive;
                productVariant.Model = model;
                productVariant.Color = color;
                productVariant.Size = size;
            }

            return await _context.SaveChangesAsync(cancellationToken);
        }

        // private async Task<int> CountProductVariantByIdAsync(string id, CancellationToken cancellationToken = default)
        // {
        //     var count = await _context.ProductVariants.CountAsync(pv => pv.ProductId == id, cancellationToken);
        //     return count;
        // }
    }
}