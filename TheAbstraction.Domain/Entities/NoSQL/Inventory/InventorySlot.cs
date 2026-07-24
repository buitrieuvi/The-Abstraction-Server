namespace TheAbstraction.Domain.Entities.NoSQL.Inventory
{
    public class InventorySlot : BaseEntityNoSQL
    {
        public string ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
