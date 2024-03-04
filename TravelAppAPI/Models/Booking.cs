using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAppAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        [BsonElement("UserId")]

        public string UserId { get; set; } = String.Empty;
        [BsonElement("PlaceId")]
        public string PlaceId { get; set; } = String.Empty;
        [BsonElement("Quantity")]
        public int Quantity { get; set; } = 0;
        [BsonElement("TotalPrice")]
        public int TotalPrice { get; set; } = 0;
        [BsonElement("Status")]
        public int Status { get; set; } = 0;
        [BsonElement("CheckInTime")]
        public DateTime CheckInTime { get; set; } = DateTime.Now;
        [BsonElement("CheckOutTime")]
        public DateTime CheckOutTime { get; set; } = DateTime.Now;
        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [BsonElement("UpdatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [BsonElement("Rating")]
        public int Rating { get; set; } = 0;
    }
}
