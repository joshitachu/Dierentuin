namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigatie-eigenschap naar Animal (een Category heeft één Animal)
        public Animal animal { get; set; }
    }
}
