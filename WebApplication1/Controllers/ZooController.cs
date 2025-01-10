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
    

        // GET: Zoos/Sunset
    public async Task<IActionResult> Sunrise()
{
    var animals = await _context.Animals.Include(a => a.Enclosure).ToListAsync();
    var results = new List<string>();

    foreach (var animal in animals)
    {
        switch (animal.activityPattern)
        {
            case Animal.ActivityPattern.Diurnal:
                results.Add($"{animal.Name} in {animal.Enclosure?.Name ?? "no enclosure"} wakes up.");
                AnimalStaus.AnimalStatuses[animal.Id] = "Awake";
                break;
            case Animal.ActivityPattern.Nocturnal:
                results.Add($"{animal.Name} in {animal.Enclosure?.Name ?? "no enclosure"} goes to sleep.");
                AnimalStaus.AnimalStatuses[animal.Id] = "Sleeping";
                break;
            default:
                results.Add($"{animal.Name} in {animal.Enclosure?.Name ?? "no enclosure"} remains active.");
                AnimalStaus.AnimalStatuses[animal.Id] = "Active";
                break;
        }
    }

    ViewData["ActionName"] = "Sunrise";
    return View("ActionResults", results);
}



public async Task<IActionResult> Sunset()
{
    var animals = await _context.Animals.Include(a => a.Enclosure).ToListAsync();
    var results = new List<string>();

    foreach (var animal in animals)
    {
        string status;
        switch (animal.activityPattern)
        {
            case Animal.ActivityPattern.Nocturnal:
                status = $"{animal.Name} wakes up!";
                AnimalStaus.UpdateStatus(animal.Id, "Awake");
                break;
            case Animal.ActivityPattern.Diurnal:
                status = $"{animal.Name} goes to sleep.";
                AnimalStaus.UpdateStatus(animal.Id, "Sleeping");
                break;
            default:
                status = $"{animal.Name} remains active.";
                AnimalStaus.UpdateStatus(animal.Id, "Active");
                break;
        }
        results.Add(status);
    }

    ViewData["ActionName"] = "Sunset";
    return View("ActionResults", results);
}

public async Task<IActionResult> FeedingTime()
{
    var animals = await _context.Animals.Include(a => a.Enclosure).ToListAsync();
    var results = new List<string>();

    foreach (var animal in animals)
    {
        string status;

        // Prioritize prey over dietary class feeding
        if (!string.IsNullOrEmpty(animal.Prey))
        {
            status = $"{animal.Name} in {animal.Enclosure?.Name ?? "no enclosure"} eats {animal.Prey} (priority over dietary class food).";
            AnimalStaus.AnimalStatuses[animal.Id] = "Eating Prey";
        }
        else
        {
            status = $"{animal.Name} in {animal.Enclosure?.Name ?? "no enclosure"} is fed according to its dietary class: {animal.Diet}.";
            AnimalStaus.AnimalStatuses[animal.Id] = "Eating Food";
        }

        results.Add(status);
    }

    ViewData["ActionName"] = "Feeding Time";
    return View("ActionResults", results);
}



public async Task<IActionResult> CheckConstraints()
{
    var enclosures = await _context.Enclosures.Include(e => e.Animals).ToListAsync();
    var results = new List<string>();

    foreach (var enclosure in enclosures)
    {
        foreach (var animal in enclosure.Animals)
        {
            double availableSpace = enclosure.Size / enclosure.Animals.Count;
            if (availableSpace < animal.SpaceRequirement)
            {
                results.Add($"{animal.Name} in {enclosure.Name} has insufficient space.");
            }

            if ((int)animal.SecurityRequirement > (int)enclosure.securityLevel)
            {
                results.Add($"{animal.Name} in {enclosure.Name} does not meet the security requirements.");
            }
        }
    }

    ViewData["ActionName"] = "Check Constraints";
    return View("ActionResults", results);
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

    var assignmentResults = new List<string>();

    foreach (var animal in unassignedAnimals)
    {
        var suitableEnclosure = zoo.Enclosures.FirstOrDefault(e =>
            e.Size >= e.Animals.Count * animal.SpaceRequirement &&
            (int)e.securityLevel >= (int)animal.SecurityRequirement);

        if (suitableEnclosure != null)
        {
            suitableEnclosure.Animals.Add(animal);
            animal.EnclosureId = suitableEnclosure.Id;
            assignmentResults.Add($"{animal.Name} assigned to existing enclosure {suitableEnclosure.Name}.");
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
            assignmentResults.Add($"{animal.Name} assigned to new enclosure {newEnclosure.Name}.");
        }
    }

    await _context.SaveChangesAsync();
    ViewData["Action"] = "Auto Assign";
    return View("ActionResult", assignmentResults);
}

    }
}
