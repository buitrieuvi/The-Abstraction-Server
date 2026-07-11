using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Application.DTOs
{
    public class ProductVariantResponseDTO : BaseDTO
    {
        public string ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

    }
}
