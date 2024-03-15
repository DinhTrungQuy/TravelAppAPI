namespace TravelAppAPI.Models
{
    public class RedisCache
    {
        public int Id { get; set; } = 0;
        public string Account_id { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
