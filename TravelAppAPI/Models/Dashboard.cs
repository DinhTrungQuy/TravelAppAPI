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

        public int Profit { get; set; } = 0;
        public int TotalUsers { get; set; } = 0;



        


    }
}
