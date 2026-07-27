using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using TheAbstraction.Application.Common.Exceptions;
using TheAbstraction.Application.Common.Interfaces;
using TheAbstraction.Domain.Entities.NoSQL;
using TheAbstraction.Domain.Entities.NoSQL.Inventory;


namespace TheAbstraction.Infrastructure.Services
{
    public class ItemService : IItemService
    {

        private readonly IMongoCollection<Item> _itemCollection;

        public ItemService(IOptions<Data.MongoDatabaseSettings> mongoDatabaseSettings, IMongoClient client)
        {
            var database = client.GetDatabase(mongoDatabaseSettings.Value.DatabaseName);
            _itemCollection = database.GetCollection<Item>(mongoDatabaseSettings.Value.ItemCollectionName);
        }

        public async Task<int> Create(string itemName, CancellationToken cancellationToken = default)
        {
            var item = await _itemCollection
                .Find(x => x.ItemName == itemName)
                .FirstOrDefaultAsync(cancellationToken);

            if (item != null)
            {
                throw new BadRequestException("Item đã tồn tại");
            }

            await _itemCollection.InsertOneAsync(new Item
            {
                ItemName = itemName
            }, cancellationToken: cancellationToken);

            return 1;
        }

        public async Task<int> Delete(string itemId, CancellationToken cancellationToken = default)
        {
            if (!ObjectId.TryParse(itemId, out var objectId))
            {
                throw new BadRequestException("Item ID không hợp lệ");
            }

            var result = await _itemCollection.DeleteOneAsync(
                x => x.Id == objectId,
                cancellationToken);

            return (int)result.DeletedCount;
        }

        public async Task<List<(string id, string itemName)>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _itemCollection
                .Find(Builders<Item>.Filter.Empty)
                .Project(x => new ValueTuple<string, string>(x.Id.ToString(), x.ItemName))
                .ToListAsync(cancellationToken);
        }

    }
}
