using System.ComponentModel.DataAnnotations;

namespace TheAbstraction.Domain.Entities
{
    public class ProductVariant : BaseEntity
    {
        [Required]
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

    }
}
