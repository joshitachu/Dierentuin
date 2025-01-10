
namespace WebApplication1.Controllers;
public static class AnimalStaus
{
    public static readonly Dictionary<int, string> AnimalStatuses = new Dictionary<int, string>();

        public static void UpdateStatus(int animalId, string status)
        {
            AnimalStatuses[animalId] = status;
        }

        public static string GetStatus(int animalId)
        {
            return AnimalStatuses.TryGetValue(animalId, out var status) ? status : "Unknown";
        }
}
