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

        public IActionResult Index()
        {
            var animals = _context.Animals.ToList(); // Haal alle dieren op uit de database
            return View(animals); // Geef de lijst door aan de view
        }

        public IActionResult AddAnimal()
        {
            return View();
        }

       
       [HttpPost]
        public IActionResult AddAnimal(Animal animal)
        {
            if (ModelState.IsValid) // Check if the submitted model is valid
            {
                _context.Animals.Add(animal); // Add the new animal to the database
                _context.SaveChanges(); // Save the changes to the database
                return RedirectToAction("Index"); // Redirect back to the Index page
            }
            
            // If the model is invalid, re-render the view with the submitted data
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
