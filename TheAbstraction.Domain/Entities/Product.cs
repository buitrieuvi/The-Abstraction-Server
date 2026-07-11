using System.ComponentModel.DataAnnotations;

namespace TheAbstraction.Domain.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; } 

        [MaxLength(1000)]
        public string Description { get; set; }

        [Range(0, 1000000)]
        public int StockQuantity { get; set; }
    }
}
