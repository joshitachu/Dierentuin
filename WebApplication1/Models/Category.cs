namespace WebApplication1.Models;

public class Category
{
    
    public int Id { get; set; }
    public string Name { get; set; }
    public Animal animal { get; set; }

    public enum Climate
    {
        Tropical, Temperate, Arcti
    }

    [Flags]
    public enum HabitatType
    {
        Forest, Aquatic, Desert, Grassland
    }

    public enum SecurityLevel
    {
        Low, Medium, High
    }
    
    public double size { get; set; }    
  
}