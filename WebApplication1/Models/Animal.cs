
namespace WebApplication1.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public string Species { get; set; }

        public Category? Category { get; set; }
        public int? CategoryId { get; set; } 

        public Size AnimalSize { get; set; } 
        public DietaryClass Diet { get; set; } 
        public ActivityPattern activityPattern { get; set; } 

        public string Prey { get; set; } 

        public int? EnclosureId { get; set; } 
        public Enclosure? Enclosure { get; set; } 

        public double SpaceRequirement { get; set; } 
        public SecurityLevel SecurityRequirement { get; set; } 

        
        public string? Status { get; set; }


        // Enums for specific attributes
        public enum Size
        {
            Microscopic,
            VerySmall,
            Small,
            Medium,
            Large,
            VeryLarge
        }

        public enum DietaryClass
        {
            Carnivore,
            Herbivore,
            Omnivore,
            Insectivore,
            Piscivore
        }

        public enum ActivityPattern
        {
            Diurnal,
            Nocturnal,
            Cathemeral
        }

      
    }
}
