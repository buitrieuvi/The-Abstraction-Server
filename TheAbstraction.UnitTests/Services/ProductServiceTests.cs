using System;
using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Commands.ProductVariant.Create;
using TheAbstraction.Infra.Data;
using TheAbstraction.Infra.Services;
using Xunit;

namespace TheAbstraction.UnitTests.Services
{
    public class ProductServiceTests
    {
        readonly ApplicationDbContext _context = new(new Microsoft.EntityFrameworkCore.DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=PC-CHAN;Database=SeverDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
            .Options);

        [Fact]
        public async Task ProductService_Create_Product()
        {

            // Arrange
            var sut = new ProductService(_context);

            var productVariants = new List<CreateProductVariantCommand>
            {
                new() {
                    Price = 10.99m,
                    Quantity = 5,
                    Model = "Model A",
                    Color = "Red",
                    Size = "M"
                },
                new() {
                    Price = 12.99m,
                    Quantity = 3,
                    Model = "Model B",
                    Color = "Blue",
                    Size = "L"
                }
            };

            // Act
            var result = await sut.CreateAsync("Test Product", "Test Description", true, productVariants, default);

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task ProductService_Delete_Product()
        {
            // Arrange
            var sut = new ProductVariantService(_context);
            await sut.DeleteProductVariantAsync("62a6394d-a031-44d8-ad38-ebaceaa49d37", default);
            await sut.DeleteProductVariantAsync("dd3666f3-4b01-4486-acdc-04bb04ff3d27", default);
            await sut.DeleteProductVariantAsync("f035f942-b99c-4269-a218-e7b033d0ad67", default);

        }
    }
}