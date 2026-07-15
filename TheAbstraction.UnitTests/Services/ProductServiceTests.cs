using System;
using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Commands.ProductVariant.Create;
using TheAbstraction.Infrastructure.Data;
using TheAbstraction.Infrastructure.Services;
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

            _context.OrderDetails.Add(new() 
            {
                OrderId = "fb2c2aed-ee05-475f-a6ee-07c12d68cc4f",
                ProductVariantId = "56f783a1-9fb3-4e46-bd4d-f48912ae579b",
                Quantity = 10,
                Price = 100
            });

            await _context.SaveChangesAsync();
        }

        [Fact]
        public async Task ProductService_Delete_Product()
        {


        }
    }
}