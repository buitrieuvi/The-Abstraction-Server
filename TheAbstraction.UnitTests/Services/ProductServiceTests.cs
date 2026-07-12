using System;
using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Commands.ProductVariant.Create;
using TheAbstraction.Infra.Data;
using TheAbstraction.Infra.Services;
using Xunit;

namespace TheAbstraction.Infra.Services.Tests;

public class ProductServiceTests
{
    ApplicationDbContext _context = new ApplicationDbContext(new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ApplicationDbContext>()
        .UseSqlServer("Server=PC-CHAN;Database=SeverDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
        .Options);

    [Fact]
    public async Task ProductService_Create_Product()
    {

        // Arrange
        var sut = new ProductService(_context);

        var productVariants = new List<CreateProductVariantCommand>
        {
            new CreateProductVariantCommand
            {
                Price = 10.99m,
                Quantity = 5,
                Model = "Model A",
                Color = "Red",
                Size = "M"
            },
            new CreateProductVariantCommand
            {
                Price = 12.99m,
                Quantity = 3,
                Model = "Model B",
                Color = "Blue",
                Size = "L"
            }
        };

        // Act
        var result = await sut.CreateAsync("Test Product", "Test Description", 10, true, productVariants, default);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public async Task ProductService_Delete_Product()
    {
        // Arrange
        var sut = new ProductService(_context);


    }
}