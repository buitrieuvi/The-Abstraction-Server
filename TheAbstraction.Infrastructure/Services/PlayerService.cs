using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities.NoSQL;
using TheAbstraction.Domain.Entities.NoSQL.Inventory;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Services
{
    public class PlayerService : IPlayerService
    {

        private readonly IMongoCollection<Player> _playerCollection;
        private readonly IMongoCollection<Inventory> _inventoryCollection;

        public PlayerService(IOptions<Data.MongoDatabaseSettings> mongoDatabaseSettings, IMongoClient client)
        {
            var database = client.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
            _playerCollection = database.GetCollection<Player>(mongoDatabaseSettings.Value.PlayerCollectionName);
            _inventoryCollection = database.GetCollection<Inventory>(mongoDatabaseSettings.Value.InventoryCollectionName);
        }

        public async Task<int> Create(string userId, string playerName)
        {
            if (!ObjectId.TryParse(userId, out var userObjectId))
            {
                throw new BadRequestException("User ID không hợp lệ");
            }

            if (string.IsNullOrWhiteSpace(playerName))
            {
                throw new BadRequestException("Tên player là bắt buộc");
            }

            var playerExist = _playerCollection.Find(x => x.PlayerName == playerName);
            if (playerExist.Any())
            {
                throw new BadRequestException("tên người chơi đã tồn tại");
            }

            Player newPlayer = new()
            {
                UserId = userObjectId,
                PlayerName = playerName
            };

            await _playerCollection.InsertOneAsync(newPlayer);

            await _inventoryCollection.InsertOneAsync(new()
            {
                PlayerId = newPlayer.Id,
                Items = []
            });

            return 1;
        }

        public async Task<int> DeletePlayerByUserId(string userId)
        {
            if (!ObjectId.TryParse(userId, out var userObjectId))
            {
                throw new BadRequestException("User ID không hợp lệ");
            }

            var player = await _playerCollection
                .Find(x => x.UserId == userObjectId)
                .FirstOrDefaultAsync();

            if (player == null)
            {
                return 0;
            }

            await _inventoryCollection.DeleteManyAsync(x => x.PlayerId == player.Id);
            var result = await _playerCollection.DeleteOneAsync(x => x.Id == player.Id);

            return (int)result.DeletedCount;
        }
    }
}
