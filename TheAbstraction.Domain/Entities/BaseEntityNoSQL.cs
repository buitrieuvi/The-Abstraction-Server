using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheAbstraction.Domain.Entities
{
    public class BaseEntityNoSQL
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}
