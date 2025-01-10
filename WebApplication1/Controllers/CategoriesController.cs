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
    public class CategoriesController : Controller
    {
        private readonly ZooDbContext _context;

        public CategoriesController(ZooDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index(string searchTerm)
{
    ViewData["SearchTerm"] = searchTerm;

    IQueryable<Category> categories = _context.Categories;

    if (!string.IsNullOrEmpty(searchTerm))
    {
        categories = categories.Where(c => c.Name.Contains(searchTerm));
    }

    var categoriesWithAnimals = await categories
        .Include(c => c.Animals) // Include navigation property
        .ToListAsync();

    return View(categoriesWithAnimals);
}

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
{
    if (!ModelState.IsValid)
    {
        // Log all validation errors
        foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
        {
            Console.WriteLine($"Validation Error: {error.ErrorMessage}");
        }
        return View(category);
    }

    _context.Add(category);
    await _context.SaveChangesAsync();
    return RedirectToAction(nameof(Index));
}


        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        // GET: Categories/AssignAnimal/5
public async Task<IActionResult> AssignAnimal(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var category = await _context.Categories
        .Include(c => c.Animals)
        .FirstOrDefaultAsync(c => c.Id == id);

    if (category == null)
    {
        return NotFound();
    }

    // Get all animals (regardless of current assignment)
    ViewData["AnimalList"] = new SelectList(_context.Animals, "Id", "Name");

    return View(category);
}


// POST: Categories/AssignAnimal/5
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> AssignAnimal(int id, int? animalId)
{
    // Fetch the category
    var category = await _context.Categories
        .Include(c => c.Animals)
        .FirstOrDefaultAsync(c => c.Id == id);

    if (category == null)
    {
        return NotFound();
    }

    // If an animal ID is provided, assign the animal to the category
    if (animalId.HasValue)
    {
        var animal = await _context.Animals.FindAsync(animalId);
        if (animal == null)
        {
            ModelState.AddModelError("AnimalId", "Invalid animal selected.");
        }
        else
        {
            // Assign the animal to the category
            animal.CategoryId = id;
            _context.Update(animal);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Details), new { id });
    }

    // If no animal ID is provided, reload the view with the updated animal list
    ViewData["AnimalList"] = new SelectList(_context.Animals, "Id", "Name");

    return View(category);
}

// GET: Categories/Search
public IActionResult Search(string searchTerm)
{
    var categories = _context.Categories
        .Include(c => c.Animals)
        .Where(c => string.IsNullOrEmpty(searchTerm) || c.Name.Contains(searchTerm))
        .ToList();

    return View(categories);
}


    }
}
