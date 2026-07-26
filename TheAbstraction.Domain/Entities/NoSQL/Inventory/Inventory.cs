using MongoDB.Bson;

namespace TheAbstraction.Domain.Entities.NoSQL.Inventory
{
    public class Inventory : BaseEntityNoSQL
    {
        public ObjectId PlayerId { get; set; }
        public List<InventorySlot> Items { get; set; } = [];
    }
}
