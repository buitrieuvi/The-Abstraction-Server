

using Azure.Core;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;
using TheAbstraction.Application.Commands.Auth;
using TheAbstraction.Application.Commands.User.Delete;
using TheAbstraction.Application.DTOs;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services;

public class UserTestAPI : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceProvider _provider;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public UserTestAPI(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _provider = factory.Services;
        _client = factory.CreateClient();
        _output = output;
    }

    [Fact]
    public async Task Test_Login()
    {
        var authResponseDTO = await Login();

        string pretty = JsonConvert.SerializeObject(authResponseDTO, Formatting.Indented);
        _output.WriteLine(pretty);
    }

    [Fact]
    public async Task GetAllUser()
    {

    }

    [Fact]
    public async Task CreateUser()
    {


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
            Id = authResponseDTO.UserId
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
            UserName = "vi",
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