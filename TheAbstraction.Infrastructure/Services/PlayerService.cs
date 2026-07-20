using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAbstraction.Application.Common.Exceptions;
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

        public async Task<int> Create(string userId, string playerName)
        {
            var playerExist = _playerCollection.Find(x => x.PlayerName == playerName);
            if (!playerExist.Any())
            {
                throw new BadRequestException("tên người chơi đã tồn tại");
            }

            await _playerCollection.InsertOneAsync(new()
            {
                UserId = userId,
                PlayerName = playerName
            });

            return 1;
        }

        public async Task<int> Delete(string playerId)
        {
            var filter = Builders<Player>.Filter.Eq(x => x.Id, new ObjectId(playerId));
            var deleteResult = await _playerCollection.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0 ? 1 : 0;
        }
        
    }
}