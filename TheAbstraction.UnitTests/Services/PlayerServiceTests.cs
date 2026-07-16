using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TheAbstraction.Infrastructure.Data;

using TheAbstraction.Infrastructure.Services;
using Xunit;
using MongoDatabaseSettings = TheAbstraction.Infrastructure.Data.MongoDatabaseSettings;

namespace TheAbstraction.Infrastructure.Services.Tests;

public class PlayerServiceTests
{

    IOptions<MongoDatabaseSettings> settings = Options.Create(new MongoDatabaseSettings
    {
        DatabaseName = "TestDb",
        PlayerCollectionName = "Player"
    });

    MongoClient client = new MongoClient("mongodb://localhost:27017");

    [Fact]
    public void PlayerService_Should_()
    {
        // Arrange
        // var sut = new PlayerService(settings, client);
            
        // // Act
        // var result = sut.Create();

        // // Assert
        // Assert.NotNull(result);
    }
}