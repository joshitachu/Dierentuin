using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ZooDbContext _context;

        public HomeController(ILogger<HomeController> logger, ZooDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Home Page: Index
        public IActionResult Index()
        {
            var zoo = _context.Zoos.FirstOrDefault();

            if (zoo == null)
            {
                ViewBag.ShowForm = true; // Indicate the form should be shown
                return View(); // Render the view for entering zoo details
            }

            return View(zoo); // Pass the existing zoo to the view
        }

        // POST: Create Zoo
        [HttpPost]
        public IActionResult CreateZoo(string name, string location)
        {
            if (_context.Zoos.Any())
            {
                return RedirectToAction(nameof(Index)); // Prevent duplicate zoos
            }

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(location))
            {
                ModelState.AddModelError("", "Both name and location are required.");
                ViewBag.ShowForm = true; // Re-show the form with errors
                return View("Index");
            }

            var zoo = new Zoo
            {
                Name = name,
                Location = location
            };

            _context.Zoos.Add(zoo);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Add Animal Page
        public IActionResult AddAnimal()
        {
            return View();
        }

        // POST: Add Animal
        [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            if (ModelState.IsValid)
            {
                _context.Animals.Add(animal);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(animal);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
