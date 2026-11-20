using MongoDB.Bson;

namespace TheAbstraction.Domain.Entities.NoSQL.Inventory
{
    public class InventorySlot : BaseNoSqlEntity
    {
        public ObjectId ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
