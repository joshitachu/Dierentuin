using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ZoosController : Controller
    {
        private readonly ZooDbContext _context;

        public ZoosController(ZooDbContext context)
        {
            _context = context;
        }

        // Existing CRUD actions...

        // GET: Zoo/Sunrise
        public async Task<IActionResult> Sunrise()
        {
            var zoo = await _context.Zoos
                .Include(z => z.Enclosures)
                .ThenInclude(e => e.Animals)
                .FirstOrDefaultAsync();

            if (zoo == null)
                return NotFound("Zoo not found.");

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    switch (animal.activityPattern)
                    {
                        case Animal.ActivityPattern.Diurnal:
                            Console.WriteLine($"{animal.Name} wakes up.");
                            break;
                        case Animal.ActivityPattern.Nocturnal:
                            Console.WriteLine($"{animal.Name} goes to sleep.");
                            break;
                        default:
                            Console.WriteLine($"{animal.Name} remains active.");
                            break;
                    }
                }
            }

            return Ok("Sunrise action completed.");
        }

        // GET: Zoos/Sunset
        public async Task<IActionResult> Sunset()
        {
            var zoo = await _context.Zoos
                .Include(z => z.Enclosures)
                .ThenInclude(e => e.Animals)
                .FirstOrDefaultAsync();

            if (zoo == null)
                return NotFound("Zoo not found.");

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    switch (animal.activityPattern)
                    {
                        case Animal.ActivityPattern.Nocturnal:
                            Console.WriteLine($"{animal.Name} wakes up.");
                            break;
                        case Animal.ActivityPattern.Diurnal:
                            Console.WriteLine($"{animal.Name} goes to sleep.");
                            break;
                        default:
                            Console.WriteLine($"{animal.Name} remains active.");
                            break;
                    }
                }
            }

            return Ok("Sunset action completed.");
        }

        // GET: Zoos/FeedingTime
        public async Task<IActionResult> FeedingTime()
        {
            var zoo = await _context.Zoos
                .Include(z => z.Enclosures)
                .ThenInclude(e => e.Animals)
                .FirstOrDefaultAsync();

            if (zoo == null)
                return NotFound("Zoo not found.");

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    if (!string.IsNullOrEmpty(animal.Prey))
                    {
                        Console.WriteLine($"{animal.Name} eats {animal.Prey}.");
                    }
                    else
                    {
                        Console.WriteLine($"{animal.Name} is fed according to its dietary class: {animal.Diet}.");
                    }
                }
            }

            return Ok("Feeding time completed.");
        }

        // GET: Zoos/CheckConstraints
        public async Task<IActionResult> CheckConstraints()
        {
            var zoo = await _context.Zoos
                .Include(z => z.Enclosures)
                .ThenInclude(e => e.Animals)
                .FirstOrDefaultAsync();

            if (zoo == null)
                return NotFound("Zoo not found.");

            foreach (var enclosure in zoo.Enclosures)
            {
                foreach (var animal in enclosure.Animals)
                {
                    double availableSpace = enclosure.Size / enclosure.Animals.Count;
                    if (availableSpace < animal.SpaceRequirement)
                    {
                        Console.WriteLine($"{animal.Name} in {enclosure.Name} has insufficient space.");
                    }

                    if ((int)animal.SecurityRequirement > (int)enclosure.securityLevel)
                    {
                        Console.WriteLine($"{animal.Name} in {enclosure.Name} does not meet the security requirements.");
                    }
                }
            }

            return Ok("Constraints checked.");
        }

        // GET: Zoo/AutoAssign
        public async Task<IActionResult> AutoAssign(bool completeExisting = true)
        {
            var zoo = await _context.Zoos
                .Include(z => z.Enclosures)
                .ThenInclude(e => e.Animals)
                .FirstOrDefaultAsync();

            if (zoo == null)
                return NotFound("Zoo not found.");

            var unassignedAnimals = await _context.Animals
                .Where(a => a.EnclosureId == null)
                .ToListAsync();

            foreach (var animal in unassignedAnimals)
            {
                var suitableEnclosure = zoo.Enclosures.FirstOrDefault(e =>
                    e.Size >= e.Animals.Count * animal.SpaceRequirement &&
                    (int)e.securityLevel >= (int)animal.SecurityRequirement);

                if (suitableEnclosure != null)
                {
                    suitableEnclosure.Animals.Add(animal);
                    animal.EnclosureId = suitableEnclosure.Id;
                }
                else
                {
                    // Create a new enclosure
                    var newEnclosure = new Enclosure
                    {
                        Name = $"Enclosure for {animal.Name}",
                        Size = animal.SpaceRequirement * 5,
                        securityLevel = animal.SecurityRequirement,
                        Animals = new List<Animal> { animal }
                    };

                    zoo.Enclosures.Add(newEnclosure);
                    _context.Enclosures.Add(newEnclosure);
                }
            }

            await _context.SaveChangesAsync();
            return Ok("AutoAssign completed.");
        }
    }
}
