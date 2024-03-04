using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAppAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Wishlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        [BsonElement("UserId")]

        public string UserId { get; set; } = String.Empty;
        [BsonElement("PlaceId")]
        public string PlaceId { get; set; } = String.Empty;
        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonElement("UpdatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}
