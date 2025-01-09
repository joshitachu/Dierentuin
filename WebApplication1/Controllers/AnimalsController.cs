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
        public async Task<IActionResult> Index()
        {
            var animals = _context.Animals.Include(a => a.Category);
            return View(await animals.ToListAsync());
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
            try
            {
                Console.WriteLine($"Creating animal: Name={animal.Name}, Species={animal.Species}, Prey={animal.Prey}, CategoryId={animal.CategoryId}");

                animal.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == animal.CategoryId);
                if (animal.Category == null)
                {
                    ModelState.AddModelError("CategoryId", "Invalid category selected.");
                }

                if (!animal.EnclosureId.HasValue)
                {
                    var defaultEnclosure = await _context.Enclosures.FirstOrDefaultAsync();
                    if (defaultEnclosure != null)
                    {
                        animal.EnclosureId = defaultEnclosure.Id;
                        animal.Enclosure = defaultEnclosure;
                    }
                    else
                    {
                        ModelState.AddModelError("EnclosureId", "No default enclosure available.");
                    }
                }

                if (ModelState.IsValid)
                {
                    _context.Add(animal);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
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

        // Actions: Sunrise
        public IActionResult Sunrise()
        {
            var animals = _context.Animals.ToList();

            foreach (var animal in animals)
            {
                switch (animal.activityPattern)
                {
                    case Animal.ActivityPattern.Diurnal:
                        Console.WriteLine($"{animal.Name} wakes up!");
                        break;
                    case Animal.ActivityPattern.Nocturnal:
                        Console.WriteLine($"{animal.Name} goes to sleep.");
                        break;
                    default:
                        Console.WriteLine($"{animal.Name} remains active.");
                        break;
                }
            }

            return Ok("Sunrise action executed for all animals.");
        }

        // Actions: Sunset
        public IActionResult Sunset()
        {
            var animals = _context.Animals.ToList();

            foreach (var animal in animals)
            {
                switch (animal.activityPattern)
                {
                    case Animal.ActivityPattern.Nocturnal:
                        Console.WriteLine($"{animal.Name} wakes up!");
                        break;
                    case Animal.ActivityPattern.Diurnal:
                        Console.WriteLine($"{animal.Name} goes to sleep.");
                        break;
                    default:
                        Console.WriteLine($"{animal.Name} remains active.");
                        break;
                }
            }

            return Ok("Sunset action executed for all animals.");
        }
    }
}
