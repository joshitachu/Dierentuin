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
    public class EnclosuresController : Controller
    {
        private readonly ZooDbContext _context;

        public EnclosuresController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: Enclosures
       public async Task<IActionResult> Index(string searchTerm)
{
    var enclosures = _context.Enclosures.AsQueryable();

    if (!string.IsNullOrEmpty(searchTerm))
    {
        enclosures = enclosures.Where(e => e.Name.Contains(searchTerm) || e.Description.Contains(searchTerm));
        ViewData["SearchTerm"] = searchTerm;
    }

    return View(await enclosures.ToListAsync());
}

        // GET: Enclosures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosures
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enclosure == null)
            {
                return NotFound();
            }

            return View(enclosure);
        }

        // GET: Enclosures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Enclosures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Size,Climate,HabitatType,SecurityLevel")] Enclosure enclosure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enclosure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(enclosure);
        }

        // GET: Enclosures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosures.FindAsync(id);
            if (enclosure == null)
            {
                return NotFound();
            }

            return View(enclosure);
        }

        // POST: Enclosures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Size,Climate,HabitatType,SecurityLevel")] Enclosure enclosure)
        {
            if (id != enclosure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enclosure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnclosureExists(enclosure.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(enclosure);
        }

        // GET: Enclosures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enclosure = await _context.Enclosures
                .Include(e => e.Animals)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (enclosure == null)
            {
                return NotFound();
            }

            return View(enclosure);
        }

        // POST: Enclosures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enclosure = await _context.Enclosures.FindAsync(id);
            if (enclosure != null)
            {
                _context.Enclosures.Remove(enclosure);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EnclosureExists(int id)
        {
            return _context.Enclosures.Any(e => e.Id == id);
        }

        // GET: Enclosures/AssignAnimal/5
public async Task<IActionResult> AssignAnimal(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var enclosure = await _context.Enclosures
        .Include(e => e.Animals)
        .FirstOrDefaultAsync(e => e.Id == id);

    if (enclosure == null)
    {
        return NotFound();
    }

    // Get all animals (regardless of current assignment)
    ViewData["AnimalList"] = new SelectList(_context.Animals, "Id", "Name");

    return View(enclosure);
}

// POST: Enclosures/AssignAnimal/5
[HttpPost]
[ValidateAntiForgeryToken]

public async Task<IActionResult> AssignAnimal(int id, int animalId)
{
    var enclosure = await _context.Enclosures
        .Include(e => e.Animals)
        .FirstOrDefaultAsync(e => e.Id == id);

    if (enclosure == null)
    {
        return NotFound();
    }

    var animal = await _context.Animals.FindAsync(animalId);
    if (animal == null)
    {
        ModelState.AddModelError("AnimalId", "Invalid animal selected.");
    }
    else
    {
        // Assign the animal to the enclosure
        animal.EnclosureId = id;
        _context.Update(animal);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction(nameof(Details), new { id });
}

// GET: Enclosures/Search
public IActionResult Search(string searchTerm)
{
    var enclosures = _context.Enclosures
        .Include(e => e.Animals)
        .Where(e => string.IsNullOrEmpty(searchTerm) || e.Name.Contains(searchTerm))
        .ToList();

    return View(enclosures);
}

// GET: Enclosures/Sunrise
public async Task<IActionResult> Sunrise()
{
    var enclosures = await _context.Enclosures
        .Include(e => e.Animals)
        .ToListAsync();

    var results = new List<string>();

    foreach (var enclosure in enclosures)
    {
        foreach (var animal in enclosure.Animals)
        {
            switch (animal.activityPattern)
            {
                case Animal.ActivityPattern.Diurnal:
                    results.Add($"{animal.Name} in {enclosure.Name} wakes up!");
                    break;
                case Animal.ActivityPattern.Nocturnal:
                    results.Add($"{animal.Name} in {enclosure.Name} goes to sleep.");
                    break;
                default:
                    results.Add($"{animal.Name} in {enclosure.Name} remains active.");
                    break;
            }
        }
    }

    ViewData["ActionName"] = "Sunrise";
    return View("ActionResults", results);
}

// GET: Enclosures/Sunset
public async Task<IActionResult> Sunset()
{
    var enclosures = await _context.Enclosures
        .Include(e => e.Animals)
        .ToListAsync();

    var results = new List<string>();

    foreach (var enclosure in enclosures)
    {
        foreach (var animal in enclosure.Animals)
        {
            switch (animal.activityPattern)
            {
                case Animal.ActivityPattern.Nocturnal:
                    results.Add($"{animal.Name} in {enclosure.Name} wakes up!");
                    break;
                case Animal.ActivityPattern.Diurnal:
                    results.Add($"{animal.Name} in {enclosure.Name} goes to sleep.");
                    break;
                default:
                    results.Add($"{animal.Name} in {enclosure.Name} remains active.");
                    break;
            }
        }
    }

    ViewData["ActionName"] = "Sunset";
    return View("ActionResults", results);
}

// GET: Enclosures/FeedingTime
public async Task<IActionResult> FeedingTime()
{
    var enclosures = await _context.Enclosures
        .Include(e => e.Animals)
        .ToListAsync();

    var results = new List<string>();

    foreach (var enclosure in enclosures)
    {
        foreach (var animal in enclosure.Animals)
        {
            if (!string.IsNullOrEmpty(animal.Prey))
            {
                results.Add($"{animal.Name} in {enclosure.Name} eats {animal.Prey}.");
            }
            else
            {
                results.Add($"{animal.Name} in {enclosure.Name} is fed according to its dietary class: {animal.Diet}.");
            }
        }
    }

    ViewData["ActionName"] = "Feeding Time";
    return View("ActionResults", results);
}


// GET: Enclosures/CheckConstraints
public async Task<IActionResult> CheckConstraints()
{
    var enclosures = await _context.Enclosures
        .Include(e => e.Animals)
        .ToListAsync();

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




    }
}
