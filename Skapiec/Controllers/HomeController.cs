using System.Diagnostics;
using Skapiec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skapiec.Entities;

namespace Skapiec.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SkapiecDBcontext dBcontext;

        public HomeController(SkapiecDBcontext dbContext, ILogger<HomeController> logger)
        {
            dBcontext = dbContext;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var productsFromDb = await dBcontext.Products.ToListAsync();
            ViewBag.Products = productsFromDb;
            return View(); // Przekieruj do widoku QueryResults.cshtml
        }
        public async Task<IActionResult> ShowResults(string queryColumn)
        {
            var productsFromDb = await dBcontext.Products
                                                .Where(p => p.query == queryColumn)
                                                .ToListAsync();
            ViewBag.Products = productsFromDb;
            return View("Index", productsFromDb); // Przekieruj do widoku Index.cshtml z wynikami zapytania
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
