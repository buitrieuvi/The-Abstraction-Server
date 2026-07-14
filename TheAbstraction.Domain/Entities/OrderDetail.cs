namespace TheAbstraction.Domain.Entities
{
    public class OrderDetail : BaseEntity
    {
        public string OrderId { get; set; }
        public string ProductVariantId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
