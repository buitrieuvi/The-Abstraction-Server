using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Json;
using TheAbstraction.Application.Commands.Auth;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services;
public class User_Service_Tests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceProvider _provider;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;
    private readonly IMapper _mapper;

    public User_Service_Tests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _provider = factory.Services;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Create_Roles() 
    {
        using var scope = _provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IIdentityService>();

        await service.CreateRoleAsync("user");
        await service.CreateRoleAsync("admin");
    }

    [Fact]
    public async Task Get_All_Roles()
    {
        using var scope = _provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IIdentityService>();

        var resutl = await service.GetRolesAsync();

        string pretty = JsonConvert.SerializeObject(resutl, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]  
    public async Task Get_All_User_Details()
    {
        using var scope = _provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IIdentityService>();

        var resutl = await service.GetAllUsersDetailsAsync();

        string pretty = JsonConvert.SerializeObject(resutl, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task Create_User()
    {
        using var scope = _provider.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IIdentityService>();

        var resutl = await service.CreateUserAsync(
            "buitrieuvi",
            "vi2000",
            "btv2000@gmail.com",
            "Bùi Triều Vĩ",
            new List<string>() 
            {
                "user", "admin"
            });

        string pretty = JsonConvert.SerializeObject(resutl, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task Login_User()
    {
        //using var scope = _provider.CreateScope();
        //var service = scope.ServiceProvider.GetRequiredService<IIdentityService>();

        //var resutl = await service.SigninUserAsync("buitrieuvi", "vi2000");

        var response = await _client.PostAsJsonAsync("api/auth/login", new AuthCommand
        {
            UserName = "buitrieuvi",
            Password = "vi2000"
        });

        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<AuthResponseDTO>();

        var pretty = JsonConvert.SerializeObject(dto, Formatting.Indented);
        _output.WriteLine(pretty);
    }
}