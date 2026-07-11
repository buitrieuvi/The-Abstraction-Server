using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace TheAbstraction.Infra.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(
            string name,
            string description,
            int stockQuantity,
            bool isActive,
            CancellationToken cancellationToken = default)
        {
            var product = new Product
            {
                Name = name,
                Description = description,
                StockQuantity = stockQuantity,
                IsActive = isActive
            };

            _context.Products.Add(product);

            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> UpdateAsync(string id, string name, string description, int stockQuantity, bool isActive, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product is null)
            {
                return 0;
            }
            product.Name = name;
            product.Description = description;
            product.StockQuantity = stockQuantity;
            product.IsActive = isActive;
            product.ModifiedDate = DateTime.UtcNow;

            var result = _context.Products.Update(product);
            
            
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (product is null)
            {
                return 0;
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ProductResponseDTO> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return product is null ? null : MapToDto(product);
        }

        public async Task<IReadOnlyList<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return products.Select(MapToDto).ToList();
        }

        private static ProductResponseDTO MapToDto(Product product)
        {
            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                StockQuantity = product.StockQuantity,
                IsActive = product.IsActive,
                CreatedDate = product.CreatedDate,
                ModifiedDate = product.ModifiedDate
            };
        }
    }


}
