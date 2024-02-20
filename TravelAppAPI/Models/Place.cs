namespace TravelAppAPI.Model
{
    public class Place
    {
        public required int id { get; set; } = 0;
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Image { get; set; } = String.Empty;
        public string Location { get; set; } = String.Empty;
        public string Rating { get; set; } = String.Empty;
        public double Price { get; set; } = 0;

    }
}
