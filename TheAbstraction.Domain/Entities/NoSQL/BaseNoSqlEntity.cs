using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheAbstraction.Domain.Entities.NoSQL
{
    public class BaseNoSqlEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
