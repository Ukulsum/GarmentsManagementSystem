using FahimKniteComposite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace FahimKniteComposite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GarmentDbContext db;
        public HomeController(GarmentDbContext _db)
        {
            this.db = _db;
        }

        ////public HomeController(ILogger<HomeController> logger)
        ////{
        ////    _logger = logger;
        ////}

        public IActionResult Index()
        {
            var image = db.products.Include(i=>i.Category).ToList();
            return View(image);
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

        public IActionResult Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = db.products.Include(c=>c.Category).Where(m => m.ProductID == id).SingleOrDefault();
            
            return View(data);
        }
    }
}