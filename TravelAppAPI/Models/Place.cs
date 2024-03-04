using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace TravelAppAPI.Model
{
    [BsonIgnoreExtraElements]
    public class Place
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = String.Empty;
        [BsonElement("DurationDays")]
        public int DurationDays { get; set; } = 0;
        [BsonElement("Name")]
        public string Name { get; set; } = String.Empty;
        [BsonElement("Description")]
        public string Description { get; set; } = String.Empty;
        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } = String.Empty;
        [BsonElement("Rating")]
        public string Rating { get; set; } = String.Empty;
        [BsonElement("Location")]
        public string Location { get; set; } = String.Empty;
        [BsonElement("Price")]
        public int Price { get; set; } = 0;
        [BsonElement("Popular")]
        public bool Popular { get; set; } = false;
        [BsonElement("Recommended")]
        public bool Recommended { get; set; } = false;
        [BsonElement("Direction")]
        public int Direction { get; set; } = 0;

    }
}
