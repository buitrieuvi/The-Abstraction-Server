using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities.NoSQL;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {

        private readonly IMongoCollection<Player> _playerCollection;

        public PlayerService(IOptions<Data.MongoDatabaseSettings> mongoDatabaseSettings, IMongoClient client)
        {
            var database = client.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
            _playerCollection = database.GetCollection<Player>(mongoDatabaseSettings.Value.PlayerCollectionName);
        }

        public async Task<int> Create(string userId, string namePlayer)
        {
            await _playerCollection.InsertOneAsync(new()
            {
                UserId = userId,
                PlayerName = namePlayer
            });

            return 1;
        }


    }
}
