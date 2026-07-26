using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TheAbstraction.Application.Common.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services;

public class Inventory_Service_Tests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceProvider _provider;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public Inventory_Service_Tests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _provider = factory.Services;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Get_Inventory() 
    {
        using IServiceScope scope = _provider.CreateScope();
        IInventoryService service = scope.ServiceProvider.GetRequiredService<IInventoryService>();

        var result = await service.Get();

        //_output.WriteLine(result.ToString());
        //List<UserDetailsResponseDTO> dtos = JsonConvert.DeserializeObject<List<UserDetailsResponseDTO>>(json);

        string pretty = JsonConvert.SerializeObject(result, Formatting.Indented);
        _output.WriteLine(pretty);
    }
}
