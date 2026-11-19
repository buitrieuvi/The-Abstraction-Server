using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Commands.ProductVariant;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Services;

public class ProductService(ApplicationDbContext context) : IProductService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> CreateAsync(string name, string? description, List<CreateProductVariantCommand> productVariants, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Tên sản phẩm là bắt buộc");

        productVariants ??= [];
        if (productVariants.Any(x => x.Price < 0 || x.Quantity < 0))
            throw new BadRequestException("Giá và số lượng không được âm");

        var product = new Product
        {
            Name = name.Trim(),
            Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim()
        };

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        _context.Products.Add(product);
        foreach (var variant in productVariants)
        {
            _context.ProductVariants.Add(new ProductVariant
            {
                ProductId = product.Id,
                Price = variant.Price,
                Quantity = variant.Quantity,
                Model = variant.Model?.Trim(),
                Color = variant.Color?.Trim(),
                Size = variant.Size?.Trim()
            });
        }

        var affected = await _context.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return affected;
    }

    public async Task<int> UpdateAsync(string id, string name, string? description, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new NotFoundException("Không tìm thấy sản phẩm");
        if (string.IsNullOrWhiteSpace(name))
            throw new BadRequestException("Tên sản phẩm là bắt buộc");

        product.Name = name.Trim();
        product.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        product.ModifiedDate = DateTime.UtcNow;
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new NotFoundException("Không tìm thấy sản phẩm");
        _context.Products.Remove(product);
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ProductResponseDTO> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new NotFoundException("Không tìm thấy sản phẩm");
        return MapToDto(product);
    }

    public async Task<IReadOnlyList<ProductResponseDTO>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _context.Products.AsNoTracking().OrderBy(x => x.Name).ToListAsync(cancellationToken);
        return products.Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<ProductResponseDTO>> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
            return await GetAllAsync(cancellationToken);
        var search = name.Trim();
        var products = await _context.Products.AsNoTracking().Where(x => x.Name.Contains(search))
            .OrderBy(x => x.Name).ToListAsync(cancellationToken);
        return products.Select(MapToDto).ToList();
    }

    private static ProductResponseDTO MapToDto(Product product) => new()
    {
        Id = product.Id,
        Name = product.Name,
        Description = product.Description
    };
}
