#nullable enable
using System.ComponentModel.DataAnnotations;

namespace TheAbstraction.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
