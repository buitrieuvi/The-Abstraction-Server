namespace TheAbstraction.Application.DTOs
{
    public class ProductResponseDTO : BaseDTO
    {
        public string Name { get; set; } 
        public string Description { get; set; }
        public int StockQuantity { get; set; }
    }
}
