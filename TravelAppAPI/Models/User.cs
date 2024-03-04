using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TravelAppAPI.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id  { get; set; } = String.Empty;
        [BsonElement("Username")]
        public string Username { get; set; } = String.Empty;
        [BsonElement("Password")]
        public string Password { get; set; } = String.Empty;
        [BsonElement("Role")]
        public string Role { get; set; } = "user";
        [BsonElement("Email")]
        public string Email { get; set; } = String.Empty;
        [BsonElement("Fullname")]
        public string Fullname { get; set; } = String.Empty;
        [BsonElement("Phone")]
        public string Phone { get; set; } = String.Empty;
        [BsonElement("ImageUrl")]
        public string ImageUrl { get; set; } = String.Empty;

    }
}
