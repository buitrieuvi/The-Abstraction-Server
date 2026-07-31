using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TheAbstraction.Application.Commands.ProductVariant;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Domain.Entities;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services;

public class Product_Service_Tests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceProvider _provider;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    private readonly IMapper _mapper;

    public Product_Service_Tests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _provider = factory.Services;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Create_Product()
    {
        using IServiceScope scope = _provider.CreateScope();

        IProductService service = scope.ServiceProvider.GetRequiredService<IProductService>();

        int result = await service.CreateAsync(
            "g",
            "gg",
            new List<CreateProductVariantCommand>
            {
                new CreateProductVariantCommand
                {
                    ProductId = "",
                    Price = 1000,
                    Quantity = 10,
                    Model = "",
                    Color = "",
                    Size = ""
                }
            }
        );

        _output.WriteLine(result.ToString());
    }

    [Fact]
    public async Task Get_All_Product()
    {
        using IServiceScope scope = _provider.CreateScope();

        IProductService service = scope.ServiceProvider.GetRequiredService<IProductService>();

        IReadOnlyList<ProductResponseDTO> result = await service.GetAllAsync();

        string pretty = JsonConvert.SerializeObject(result, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task Get_All_ProductVariant()
    {
        using IServiceScope scope = _provider.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<IProductVariantService>();

        IReadOnlyList<ProductVariantResponseDTO> result = await service.GetProductVariantAsync();

        string pretty = JsonConvert.SerializeObject(result, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task Get_ProductVariant_By_Id()
    {
        using IServiceScope scope = _provider.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<IProductVariantService>();

        ProductVariantResponseDTO result = await service.GetProductVariantById("6a686bb04e62dd8f39019321");

        string pretty = JsonConvert.SerializeObject(result, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task Update_ProductVariant()
    {
        using IServiceScope scope = _provider.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<IProductVariantService>();

        var result = await service.UpdateProductVariantAsync(
            "6a686bb04e62dd8f39019321",
            9,
            9,
            "1",
            "1",
            "1"

        );

        string pretty = JsonConvert.SerializeObject(result, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task Delete_ProductVariant() { }

    [Fact]
    public async Task Delete_Product() { }

}
