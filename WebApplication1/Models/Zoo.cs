namespace WebApplication1.Models
{
    public class Zoo
    {
        public int Id { get; set; } // Primary key

        public string Name { get; set; } = string.Empty; // Ensure non-nullable strings are initialized
        public string Location { get; set; } = string.Empty;

        public List<Enclosure> Enclosures { get; set; } = new List<Enclosure>(); // Add relationship to enclosures
    }
}
