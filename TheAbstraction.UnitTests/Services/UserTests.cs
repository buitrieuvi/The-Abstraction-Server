using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using Xunit;
using Xunit.Abstractions;

namespace TheAbstraction.UnitTests.Services;

public class UserTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly IServiceProvider _provider;
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public UserTests(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _provider = factory.Services;
        _client = factory.CreateClient();
        _output = output;
    }

    //public async Task<string> Get(string api) 
    //{
    //    var response = await _client.GetAsync(api);

    //    response.EnsureSuccessStatusCode();

    //    var json = await response.Content.ReadAsStringAsync();
    //    var pretty = JsonSerializer.Serialize(
    //        JsonSerializer.Deserialize<object>(json),
    //        new JsonSerializerOptions
    //        {
    //            WriteIndented = true
    //        });

    //    _output.WriteLine(pretty);
    //    return pretty;
    //}

    [Fact]
    public async Task UserGetAll()
    {
        using var scope = _provider.CreateScope();

        var identityService = scope.ServiceProvider
            .GetRequiredService<IIdentityService>();

        var users = await identityService.GetAllUsersAsync();
        var userList = new List<UserResponseDTO>();
        users.ForEach(user => 
        {
            userList.Add(new() 
            {
                Id = user.id,
                FullName = user.fullName,
                UserName = user.userName,
                Email = user.email
            });
        });

        var json = JsonConvert.SerializeObject(userList, Formatting.Indented);

        _output.WriteLine(json);
    }
}