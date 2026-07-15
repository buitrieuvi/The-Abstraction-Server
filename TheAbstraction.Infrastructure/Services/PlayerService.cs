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

        public PlayerService(IOptions<MonggoDatabaseSettings> monggoDatabaseSettings, IMongoClient client)
        {
            var database = client.GetDatabase(monggoDatabaseSettings.Value.DatabaseName);
            _playerCollection = database.GetCollection<Player>(monggoDatabaseSettings.Value.PlayerCollectionName);
        }

        public async Task<int> Create(string namePlayer)
        {
            await _playerCollection.InsertOneAsync(new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                PlayerName = namePlayer
            });

            return 1;
        }


    }
}
