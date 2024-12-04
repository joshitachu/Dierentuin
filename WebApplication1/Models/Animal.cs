namespace WebApplication1.Models;

public class Animal
{
    
      
    public int Id { get; set; }
    public string Name { get; set; }
    public string species { get; set; }
    public Category category { get; set; }


    public enum Size
    {
        Microscopic, VerySmall, Small, Medium, Large, VeryLar
    }

    public enum DietaryClass
    {
        Carnivore, Herbivore, Omnivore, Insectivore, Piscivore
    }

    public enum ActivityPattern
    {
        Diurnal, Nocturnal, Cathemer
    }
    
    public string prey { get; set; }
    
    public Enclosure enclosure { get; set; }



    
}