

using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using TheAbstraction.Application.Commands.Auth;
using TheAbstraction.Application.Commands.InventoryItem.Create;
using TheAbstraction.Application.Commands.ProductVariant.Create;
using TheAbstraction.Application.Commands.User.Delete;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Application.Queries.User;
using TheAbstraction.Domain.Entities;
using TheAbstraction.Infrastructure.Data;
using TheAbstraction.Infrastructure.Services;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services;

public class User_Service_Tests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceProvider _provider;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public User_Service_Tests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _provider = factory.Services;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Test_API_Login()
    {
        var authResponseDTO = await Login();

        string pretty = JsonConvert.SerializeObject(authResponseDTO, Formatting.Indented);
        _output.WriteLine(pretty);

        //using var scope = _provider.CreateScope();
        //var service = scope.ServiceProvider.GetRequiredService<IdentityService>();

        //var resutl = await service.SigninUserAsync("buitreuvi", "vi2000");
        //Assert.True(resutl);
    }



    [Fact]
    public async Task GetAllUser()
    {
        var response = await _client.GetAsync("/api/user/GetAllUserDetails");
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        List<UserDetailsResponseDTO> dtos = JsonConvert.DeserializeObject<List<UserDetailsResponseDTO>>(json);

        string pretty = JsonConvert.SerializeObject(dtos, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task CreateUser()
    {
        //using var scope = _provider.CreateScope();
        //var service = scope.ServiceProvider.GetRequiredService<IProductService>();

        //await service.CreateAsync(
        //    "1",
        //    "1",
        //    true,
        //    [
        //        new CreateProductVariantCommand() 
        //        {
        //            Price = 10,
        //            Quantity = 1,
        //            Model ="A"

        //        },
        //        new CreateProductVariantCommand() 
        //        {
        //            Price = 100,
        //            Quantity = 2,
        //            Model ="B"
        //        },
        //    ]
        //);

    }

    [Fact]
    public async Task UpdateUser()
    {

    }

    [Fact]
    public async Task DeleteUser()
    {
        var authResponseDTO = await Login();

        DeleteUserCommand request = new()
        {
            UserId = authResponseDTO.UserId
        };

        var httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"/api/user/delete/{authResponseDTO.UserId}")
        {
            Content = JsonContent.Create(request)
        };

        var response = await _client.SendAsync(httpRequest);

        response.EnsureSuccessStatusCode();
    }

    private async Task<AuthResponseDTO> Login() 
    {
        AuthCommand request = new()
        {
            UserName = "buitrieuvi",
            Password = "vi2000"
        };

        var response = await _client.PostAsJsonAsync(
            "/api/auth/login",
            request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        AuthResponseDTO obj = JsonConvert.DeserializeObject<AuthResponseDTO>(json);
        return obj;
    }
}