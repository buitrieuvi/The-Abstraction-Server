namespace TheAbstraction.Domain.Entities
{
    public class BaseEntityNoId
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
