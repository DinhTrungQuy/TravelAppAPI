using MongoDB.Bson.Serialization.Attributes;

namespace TravelAppAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        [BsonElement("PlaceId")]
        public string PlaceId { get; set; } = String.Empty;
        [BsonElement("UserId")]
        public string UserId { get; set; } = String.Empty;
        [BsonElement("RatingValue")]
        public int RatingValue { get; set; } = 0;
        [BsonElement("Comment")]
        public string Comment { get; set; } = String.Empty;
    }
}
