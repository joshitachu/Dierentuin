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
       
public async Task<IActionResult> Create([Bind("Name,Description,Size,securityLevel,Habitat,Climate")] Enclosure enclosure)
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
       
public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Size,securityLevel,Habitat,Climate")] Enclosure enclosure)
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
[HttpGet("/Enclosures/{id:int}/Sunrise")]
public async Task<IActionResult> Sunrise(int id)
{
    var enclosure = await _context.Enclosures
        .Include(e => e.Animals)
        .FirstOrDefaultAsync(e => e.Id == id);

    if (enclosure == null)
        return NotFound("Enclosure not found.");

    var results = new List<string>();
    foreach (var animal in enclosure.Animals)
    {
        string status;
        switch (animal.activityPattern)
        {
            case Animal.ActivityPattern.Diurnal:
                status = $"{animal.Name} wakes up in {enclosure.Name}!";
                AnimalStaus.AnimalStatuses[animal.Id] = "Awake";
                break;
            case Animal.ActivityPattern.Nocturnal:
                status = $"{animal.Name} goes to sleep in {enclosure.Name}.";
                AnimalStaus.AnimalStatuses[animal.Id] = "Sleeping";
                break;
            default:
                status = $"{animal.Name} remains active in {enclosure.Name}.";
                AnimalStaus.AnimalStatuses[animal.Id] = "Active";
                break;
        }
        results.Add(status);
    }

    ViewData["ActionName"] = $"Sunrise in {enclosure.Name}";
    return View("ActionResults", results);
}

[HttpGet("/Enclosures/{id:int}/Sunset")]
public async Task<IActionResult> Sunset(int id)
{
    var enclosure = await _context.Enclosures
        .Include(e => e.Animals)
        .FirstOrDefaultAsync(e => e.Id == id);

    if (enclosure == null)
        return NotFound("Enclosure not found.");

    var results = new List<string>();
    foreach (var animal in enclosure.Animals)
    {
        string status;
        switch (animal.activityPattern)
        {
            case Animal.ActivityPattern.Nocturnal:
                status = $"{animal.Name} wakes up in {enclosure.Name}!";
                AnimalStaus.AnimalStatuses[animal.Id] = "Awake";
                break;
            case Animal.ActivityPattern.Diurnal:
                status = $"{animal.Name} goes to sleep in {enclosure.Name}.";
                AnimalStaus.AnimalStatuses[animal.Id] = "Sleeping";
                break;
            default:
                status = $"{animal.Name} remains active in {enclosure.Name}.";
                AnimalStaus.AnimalStatuses[animal.Id] = "Active";
                break;
        }
        results.Add(status);
    }

    ViewData["ActionName"] = $"Sunset in {enclosure.Name}";
    return View("ActionResults", results);
}

[HttpGet("/Enclosures/{id:int}/FeedingTime")]
public async Task<IActionResult> FeedingTime(int id)
{
    var enclosure = await _context.Enclosures
        .Include(e => e.Animals)
        .FirstOrDefaultAsync(e => e.Id == id);

    if (enclosure == null)
        return NotFound("Enclosure not found.");

    var results = new List<string>();

    foreach (var animal in enclosure.Animals)
    {
        string status;

        // Prioritize prey over dietary class feeding
        if (!string.IsNullOrEmpty(animal.Prey))
        {
            status = $"{animal.Name} eats {animal.Prey} in {enclosure.Name} (priority over dietary class food).";
            AnimalStaus.AnimalStatuses[animal.Id] = "Eating Prey";
        }
        else
        {
            status = $"{animal.Name} is fed according to its dietary class: {animal.Diet} in {enclosure.Name}.";
            AnimalStaus.AnimalStatuses[animal.Id] = "Eating Food";
        }

        results.Add(status);
    }

    ViewData["ActionName"] = $"Feeding Time in {enclosure.Name}";
    return View("ActionResults", results);
}


[HttpGet("/Enclosures/{id:int}/CheckConstraints")]
public async Task<IActionResult> CheckConstraints(int id)
{
    var enclosure = await _context.Enclosures
        .Include(e => e.Animals)
        .FirstOrDefaultAsync(e => e.Id == id);

    if (enclosure == null)
        return NotFound("Enclosure not found.");

    var results = new List<string>();
    foreach (var animal in enclosure.Animals)
    {
        string status;
        double availableSpace = enclosure.Size / enclosure.Animals.Count;
        if (availableSpace < animal.SpaceRequirement)
        {
            status = $"{animal.Name} has insufficient space in {enclosure.Name}.";
        }
        else
        {
            status = $"{animal.Name} has sufficient space in {enclosure.Name}.";
        }

        if ((int)animal.SecurityRequirement > (int)enclosure.securityLevel)
        {
            status += $" Security requirements are not met.";
        }
        else
        {
            status += $" Security requirements are met.";
        }
        results.Add(status);
        AnimalStaus.AnimalStatuses[animal.Id] = "Checked";
    }

    ViewData["ActionName"] = $"Check Constraints in {enclosure.Name}";
    return View("ActionResults", results);
}




    }
}
