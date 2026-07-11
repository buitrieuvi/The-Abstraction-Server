namespace TheAbstraction.Application.DTOs
{
    public class BaseDTO
    {
        public string Id { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}