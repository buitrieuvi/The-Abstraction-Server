using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Commands.ProductVariant;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Services
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<int> CreateAsync(
            string name,
            string description,
            bool isActive,
            List<CreateProductVariantCommand> productVariants,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new BadRequestException("Tên sản phẩm là bắt buộc");
            }

            productVariants ??= [];
            var product = new Product
            {
                Name = name,
                Description = description,
                IsActive = isActive
            };

            await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            _context.Products.Add(product);
            await _context.SaveChangesAsync(cancellationToken);

            foreach (var productVariant in productVariants)
            {
                _context.ProductVariants.Add(new ProductVariant
                {
                    ProductId = product.Id,
                    Price = productVariant.Price,
                    Quantity = productVariant.Quantity,
                    Model = productVariant.Model,
                    Color = productVariant.Color,
                    Size = productVariant.Size
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
            return 1;
        }

        public async Task<int> UpdateAsync(string id, string name, string description, bool isActive, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new NotFoundException("Không tìm thấy sản phẩm");

            product.Name = name;
            product.Description = description;
            product.IsActive = isActive;
            product.ModifiedDate = DateTime.UtcNow;

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new NotFoundException("Không tìm thấy sản phẩm");
            var variants = _context.ProductVariants.Where(x => x.ProductId == id);

            _context.ProductVariants.RemoveRange(variants);
            _context.Products.Remove(product);

            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task<ProductResponseDTO> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                ?? throw new NotFoundException("Không tìm thấy sản phẩm");
            return MapToDto(product);
        }

        private static ProductResponseDTO MapToDto(Product product) => new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            IsActive = product.IsActive
        };

        public async Task<IReadOnlyList<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _context.Products
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return [.. products.Select(MapToDto)];
        }
        public async Task<IReadOnlyList<ProductResponseDTO>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var products = await _context.Products
                .Where(x => x.Name.Contains(name ?? string.Empty))
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return [.. products.Select(MapToDto)];
        }
        
    }

}
