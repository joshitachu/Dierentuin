using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly ZooDbContext _context;

        public AnimalsController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: Animals
     public async Task<IActionResult> Index(string searchTerm)
{
    ViewData["SearchTerm"] = searchTerm;

    // Fetch animals with related data
    var animals = await _context.Animals
        .Include(a => a.Category)
        .Include(a => a.Enclosure)
        .ToListAsync();

    // Retrieve or assign default status from AnimalStatuses
    var dynamicAnimals = animals.Select(a =>
    {
        // If no status exists for the animal, assign a default status
        if (!AnimalStatuses.TryGetValue(a.Id, out var status))
        {
            status = a.Enclosure == null ? "Unassigned" : "Active";
            AnimalStatuses[a.Id] = status; // Store default status
        }

        return new
        {
            a.Id,
            a.Name,
            a.Species,
            CategoryName = a.Category?.Name ?? "No Category",
            EnclosureName = a.Enclosure?.Name ?? "No Enclosure",
            a.AnimalSize,
            a.Diet,
            a.activityPattern,
            a.Prey,
            a.SpaceRequirement,
            a.SecurityRequirement,
            Status = status // Status from AnimalStatuses
        };
    }).ToList();

    // Apply search filter if needed
    if (!string.IsNullOrEmpty(searchTerm))
    {
        dynamicAnimals = dynamicAnimals
            .Where(a =>
                a.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                a.Species.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                a.CategoryName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                a.EnclosureName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    return View(dynamicAnimals);
}



        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Category)
                .Include(a => a.Enclosure)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["CategoryList"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["EnclosureList"] = new SelectList(_context.Enclosures, "Id", "Name");
            return View();
        }

        // POST: Animals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal animal)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    animal.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == animal.CategoryId);
                    animal.Enclosure = await _context.Enclosures.FirstOrDefaultAsync(e => e.Id == animal.EnclosureId);

                    _context.Add(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating animal: {ex.Message}");
                }
            }

            ViewData["CategoryList"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
            ViewData["EnclosureList"] = new SelectList(_context.Enclosures, "Id", "Name", animal.EnclosureId);
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            ViewData["CategoryList"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
            ViewData["EnclosureList"] = new SelectList(_context.Enclosures, "Id", "Name", animal.EnclosureId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            ViewData["CategoryList"] = new SelectList(_context.Categories, "Id", "Name", animal.CategoryId);
            ViewData["EnclosureList"] = new SelectList(_context.Enclosures, "Id", "Name", animal.EnclosureId);
            return View(animal);
        }


        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Category)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(a => a.Id == id);
        }

  

// Actions: Sunset for Individual Animal
private static readonly Dictionary<int, string> AnimalStatuses = new Dictionary<int, string>();

    public async Task<IActionResult> Sunrise(int id)
    {
        var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);

        if (animal == null)
            return NotFound("Animal not found.");

        string status;
        switch (animal.activityPattern)
        {
            case Animal.ActivityPattern.Diurnal:
                status = $"{animal.Name} wakes up!";
                AnimalStatuses[id] = "Awake"; // Set dynamic status
                break;
            case Animal.ActivityPattern.Nocturnal:
                status = $"{animal.Name} goes to sleep.";
                AnimalStatuses[id] = "Sleeping"; // Set dynamic status
                break;
            default:
                status = $"{animal.Name} remains active.";
                AnimalStatuses[id] = "Active"; // Set dynamic status
                break;
        }

        ViewData["ActionName"] = "Sunrise";
        ViewData["AnimalName"] = animal.Name;
        return View("ActionResult", status);
    }

    public async Task<IActionResult> Sunset(int id)
    {
        var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);

        if (animal == null)
            return NotFound("Animal not found.");

        string status;
        switch (animal.activityPattern)
        {
            case Animal.ActivityPattern.Nocturnal:
                status = $"{animal.Name} wakes up!";
                AnimalStatuses[id] = "Awake"; // Set dynamic status
                break;
            case Animal.ActivityPattern.Diurnal:
                status = $"{animal.Name} goes to sleep.";
                AnimalStatuses[id] = "Sleeping"; // Set dynamic status
                break;
            default:
                status = $"{animal.Name} remains active.";
                AnimalStatuses[id] = "Active"; // Set dynamic status
                break;
        }

        ViewData["ActionName"] = "Sunset";
        ViewData["AnimalName"] = animal.Name;
        return View("ActionResult", status);
    }

    public async Task<IActionResult> FeedingTime(int id)
    {
        var animal = await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);

        if (animal == null)
            return NotFound("Animal not found.");

        string status;
        if (!string.IsNullOrEmpty(animal.Prey))
        {
            status = $"{animal.Name} eats {animal.Prey}.";
            AnimalStatuses[id] = "Eating"; // Set dynamic status
        }
        else
        {
            status = $"{animal.Name} is fed according to its dietary class: {animal.Diet}.";
            AnimalStatuses[id] = "Eating"; // Set dynamic status
        }

        ViewData["ActionName"] = "Feeding Time";
        ViewData["AnimalName"] = animal.Name;
        return View("ActionResult", status);
    }


// Actions: CheckConstraints for Individual Animal
public async Task<IActionResult> CheckConstraints(int id)
{
    var animal = await _context.Animals.Include(a => a.Enclosure).FirstOrDefaultAsync(a => a.Id == id);

    if (animal == null)
        return NotFound("Animal not found.");

    string status = "Status checked";
    List<string> messages = new();

    if (animal.Enclosure == null)
    {
        messages.Add($"{animal.Name} is not assigned to any enclosure.");
        animal.Status = "Unassigned";
    }
    else
    {
        double availableSpace = animal.Enclosure.Size / animal.Enclosure.Animals.Count;
        if (availableSpace < animal.SpaceRequirement)
        {
            messages.Add($"{animal.Name} has insufficient space in {animal.Enclosure.Name}.");
        }
        else
        {
            messages.Add($"{animal.Name} has sufficient space in {animal.Enclosure.Name}.");
        }

        if ((int)animal.SecurityRequirement > (int)animal.Enclosure.securityLevel)
        {
            messages.Add($"{animal.Name} does not meet the security requirements in {animal.Enclosure.Name}.");
        }
        else
        {
            messages.Add($"{animal.Name} meets the security requirements in {animal.Enclosure.Name}.");
        }

        animal.Status = "Checked"; // Update status
    }

    // Save the updated status to the database
    _context.Update(animal);
    await _context.SaveChangesAsync();

    ViewData["ActionName"] = "Check Constraints";
    ViewData["AnimalName"] = animal.Name;
    return View("ActionResultList", messages);
}


[HttpPost]
public async Task<IActionResult> SetStatus(int id, string status)
{
    var animal = await _context.Animals.FindAsync(id);
    if (animal == null)
    {
        return NotFound("Animal not found.");
    }

    animal.Status = status;
    _context.Update(animal);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}



    }
}
