namespace TheAbstraction.Domain.Entities
{
    public enum OrderStatus
    {
        Pending,     // Chờ xử lý
        Processing,  // Đang xử lý
        Shipped,     // Đã giao hàng
        Delivered,   // Đã nhận
        Cancelled    // Đã hủy
    }
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    }
}
