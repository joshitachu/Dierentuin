namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigatie-eigenschap naar Animal (een Category heeft ��n Animal)
              public ICollection<Animal> Animals { get; set; }


    }
}
