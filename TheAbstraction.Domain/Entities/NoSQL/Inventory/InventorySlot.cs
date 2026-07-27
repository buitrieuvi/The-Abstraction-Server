using MongoDB.Bson;

namespace TheAbstraction.Domain.Entities.NoSQL.Inventory
{
    public class InventorySlot : BaseEntityNoSQL
    {
        public ObjectId ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
