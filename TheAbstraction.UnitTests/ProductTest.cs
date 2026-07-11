using Microsoft.EntityFrameworkCore;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Infra.Data;
using TheAbstraction.Infra.Services;
using Xunit;

namespace TheAbstraction.UnitTests;

public class ProductTest
{
    [Fact]
    public async Task GetAllProducts()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=PC-CHAN;Database=SeverDB;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True")
            .Options;

        using var context = new ApplicationDbContext(options);

        var service = new ProductService(context);

        var products = await service.GetAllAsync();

        Assert.Equal(2, products.Count);
    }
}