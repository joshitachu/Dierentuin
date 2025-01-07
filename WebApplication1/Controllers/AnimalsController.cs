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
            var zooDbContext = _context.Animals.Include(a => a.category);
            return View(await zooDbContext.ToListAsync());
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
public IActionResult Create()
{
    ViewData["CategoryList"] = new SelectList(_context.Categories, "Id", "Name"); // Populate categories
    ViewData["EnclosureList"] = new SelectList(_context.Enclosures, "Id", "Name"); // Populate enclosures
    return View();
}


[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Animal animal)
{
    try
    {
        Console.WriteLine($"Attempting to create an animal: Name={animal.Name}, Species={animal.species}, Prey={animal.prey}, CategoryId={animal.CategoryId}");

        // Fetch the Category object
        animal.category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == animal.CategoryId);
        if (animal.category == null)
        {
            ModelState.AddModelError("CategoryId", "Invalid category selected.");
        }

        // Assign a default Enclosure if not provided
        if (!animal.EnclosureId.HasValue)
        {
            var defaultEnclosure = await _context.Enclosures.FirstOrDefaultAsync();
            if (defaultEnclosure == null)
            {
                ModelState.AddModelError("EnclosureId", "No default enclosure available.");
            }
            else
            {
                animal.EnclosureId = defaultEnclosure.Id;
                animal.enclosure = defaultEnclosure;
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", animal.CategoryId);
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,species,CategoryId,prey")] Animal animal)
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
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", animal.CategoryId);
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
                .Include(a => a.category)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}
