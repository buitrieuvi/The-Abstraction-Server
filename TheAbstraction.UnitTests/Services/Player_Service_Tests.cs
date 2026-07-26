using Microsoft.Extensions.DependencyInjection;
using TheAbstraction.Application.Common.Interfaces;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services;

public class Player_Service_Tests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceProvider _provider;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public Player_Service_Tests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _provider = factory.Services;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Create_Player() 
    {
        using IServiceScope scope = _provider.CreateScope();
        IPlayerService service = scope.ServiceProvider.GetRequiredService<IPlayerService>();

        int result = await service.Create("6a5ffafeb25058ca2601b45f", "vi 2000");
        _output.WriteLine(result.ToString());
    }

    [Fact]
    public async Task Delete_Player()
    {
        using IServiceScope scope = _provider.CreateScope();
        IPlayerService playerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();
        IInventoryService inventoryService = scope.ServiceProvider.GetRequiredService<IInventoryService>();

        int playerResult = await playerService.DeletePlayerByUserId("6a5ffafeb25058ca2601b45f");
        //int inventoryResult = await playerService.DeletePlayerByUserId("6a5ffafeb25058ca2601b45f");
        _output.WriteLine(playerResult.ToString());
    }
}
