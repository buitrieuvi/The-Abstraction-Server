namespace TheAbstraction.Domain.Entities.NoSQL.Inventory
{
    public class Inventory : BaseEntityNoSQL
    {
        public string PlayerId { get; set; } 

        public List<InventorySlot> Items { get; set; }
    }
}
