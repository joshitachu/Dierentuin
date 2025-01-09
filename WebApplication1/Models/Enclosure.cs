namespace WebApplication1.Models
{
    public class Enclosure
    {
        public int Id { get; set; } // Primary key

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public double Size { get; set; } // Space in square meters
        public SecurityLevel securityLevel { get; set; }
        public HabitatType Habitat { get; set; }
        public ClimateType Climate { get; set; }



        public List<Animal> Animals { get; set; } = new List<Animal>(); // Relationship with animals

        // Enums
        public enum HabitatType
        {
            Forest,
            Aquatic,
            Desert,
            Grassland
        }

        public enum ClimateType
        {
            Tropical,
            Temperate,
            Arctic
        }

        
    }
}
