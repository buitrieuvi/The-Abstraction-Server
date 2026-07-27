using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Application.DTOs;
using TheAbstraction.Domain.Entities.NoSQL;
using TheAbstraction.Domain.Entities.NoSQL.Inventory;

namespace TheAbstraction.Infrastructure.Services
{
    public class InventoryService : IInventoryService
    {

        private readonly IMongoCollection<Inventory> _inventoryCollection;
        private readonly IMongoCollection<Item> _itemCollection;
        private readonly IMapper _mapper;

        public InventoryService(
            IOptions<Data.MongoDatabaseSettings> mongoDatabaseSettings,
            IMongoClient client,
            IMapper mapper)
        {
            var database = client.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
            _inventoryCollection = database.GetCollection<Inventory>(mongoDatabaseSettings.Value.InventoryCollectionName);
            _itemCollection = database.GetCollection<Item>(mongoDatabaseSettings.Value.ItemCollectionName);
            _mapper = mapper;
        }

        public async Task<int> Create(string playerId, CancellationToken cancellationToken = default)
        {
            var playerObjectId = ParseObjectId(playerId, "Player ID");
            var exists = await _inventoryCollection
                .Find(x => x.PlayerId == playerObjectId)
                .AnyAsync(cancellationToken);

            if (exists)
            {
                throw new BadRequestException("Inventory của player đã tồn tại");
            }

            await _inventoryCollection.InsertOneAsync(new Inventory
            {
                PlayerId = playerObjectId,
                Items = []
            }, cancellationToken: cancellationToken);

            return 1;
        }

        public async Task<int> Update(string playerId, string itemId, int newQuantity, CancellationToken cancellationToken = default)
        {
            if (newQuantity < 0)
            {
                throw new BadRequestException("Số lượng không được âm");
            }

            var playerObjectId = ParseObjectId(playerId, "Player ID");
            var itemObjectId = ParseObjectId(itemId, "Item ID");
            var inventory = await _inventoryCollection
                .Find(x => x.PlayerId == playerObjectId)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException("Không tìm thấy inventory của player");

            inventory.Items ??= [];
            var slot = inventory.Items.FirstOrDefault(x => x.ItemId == itemObjectId);

            if (newQuantity == 0)
            {
                if (slot == null)
                {
                    return 0;
                }

                inventory.Items.Remove(slot);
            }
            else if (slot != null)
            {
                slot.Quantity = newQuantity;
                slot.ModifiedDate = DateTime.UtcNow;
            }
            else
            {
                var itemExists = await _itemCollection
                    .Find(x => x.Id == itemObjectId)
                    .AnyAsync(cancellationToken);

                if (!itemExists)
                {
                    throw new NotFoundException("Không tìm thấy item");
                }

                inventory.Items.Add(new InventorySlot { ItemId = itemObjectId, Quantity = newQuantity });
            }

            inventory.ModifiedDate = DateTime.UtcNow;
            var result = await _inventoryCollection.ReplaceOneAsync(
                x => x.Id == inventory.Id,
                inventory,
                cancellationToken: cancellationToken);

            return (int)result.ModifiedCount;
        }

        public async Task<int> Update(string playerId, List<InventorySlot> newItems, CancellationToken cancellationToken = default)
        {
            var objectId = ParseObjectId(playerId, "Player ID");
            if (newItems == null || newItems.Any(x => x.Quantity < 0))
            {
                throw new BadRequestException("Danh sách item không hợp lệ");
            }

            var result = await _inventoryCollection.UpdateOneAsync(
                x => x.PlayerId == objectId,
                Builders<Inventory>.Update
                    .Set(x => x.Items, newItems)
                    .Set(x => x.ModifiedDate, DateTime.UtcNow),
                cancellationToken: cancellationToken);

            return (int)result.ModifiedCount;
        }

        public async Task<List<InventoryResponseDTO>> Get(CancellationToken cancellationToken = default)
        {
            var result = await _inventoryCollection
                .Find(Builders<Inventory>.Filter.Empty)
                .ToListAsync(cancellationToken);

            return result.Select(inventory => new InventoryResponseDTO
            {
                PlayerId = inventory.PlayerId.ToString(),
                Items = (inventory.Items ?? [])
                    .Select(item => new ItemInventoryResponseDTO
                    {
                        ItemId = item.ItemId.ToString(),
                        Quantity = item.Quantity
                    })
                    .ToList()
            }).ToList();
        }

        public async Task<int> DeleteInventoryByUserId(string userId, CancellationToken cancellationToken = default)
        {
            var playerId = ParseObjectId(userId, "Player ID");
            var result = await _inventoryCollection.DeleteManyAsync(
                Builders<Inventory>.Filter.Eq(x => x.PlayerId, playerId),
                cancellationToken);
            return (int)result.DeletedCount;
        }

        private static ObjectId ParseObjectId(string id, string name)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                throw new BadRequestException($"{name} không hợp lệ");
            }

            return objectId;
        }
    }
}
