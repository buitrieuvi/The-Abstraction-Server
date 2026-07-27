namespace TheAbstraction.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
    }
}
