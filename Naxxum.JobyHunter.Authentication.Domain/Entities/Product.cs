using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Range(0, 1000000000)]
        public decimal Price { get; set; }

        [Range(0, 1000000)]
        public int StockQuantity { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
