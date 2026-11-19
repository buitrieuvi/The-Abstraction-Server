#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAbstraction.Application.DTOs
{
    public class ProductVariantResponseDTO
    {
        public string Id { get; set; } = string.Empty;
        public string ProductId { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }

    }
}
