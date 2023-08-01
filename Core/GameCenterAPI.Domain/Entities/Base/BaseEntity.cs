using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameCenterAPI.Domain.Entities.Base
{
    public class BaseEntity
    {
        [BsonId]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
