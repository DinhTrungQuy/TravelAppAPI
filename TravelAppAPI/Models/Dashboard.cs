using MongoDB.Bson.Serialization.Attributes;

namespace TravelAppAPI.Models
{
    [BsonIgnoreExtraElements]
    public class Dashboard
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        [BsonElement("Date")]
        public DateTime Date { get; set; } = DateTime.Today;
        [BsonElement("Profit")]
        public int Profit { get; set; } = 0;
        [BsonElement("TotalUsers")]        
        public int TotalUsers { get; set; } = 0;
        [BsonElement("TotalPlaces")]
        public int TotalPlaces { get; set; } = 0;
        [BsonElement("TotalBookings")]
        public int TotalBookings { get; set; } = 0;




        


    }
}
