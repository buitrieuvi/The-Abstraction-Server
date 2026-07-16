using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TheAbstraction.Domain.Entities.NoSQL
{
    public class Player : BaseEntityNoSQL
    {
        public string UserId { get; set; }
        public string PlayerName { get; set; }
    }
}
