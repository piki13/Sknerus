using System.Diagnostics;
using Skapiec.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skapiec.Entities;
using System.Linq;

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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var AllproductsFromDb = await dBcontext.Products.ToListAsync();

            ViewBag.Products = AllproductsFromDb;
            return View(); // Przekieruj do widoku QueryResults.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Index(string queryColumn, string sortOrder)
        {
            var AllproductsFromDb = await dBcontext.Products.ToListAsync();
            var productsFromDb = await dBcontext.Products
                                                .Where(p => p.query == queryColumn)
                                                .ToListAsync();

            // Sortowanie
            switch (sortOrder)
            {
                case "NameDesc":
                    productsFromDb = productsFromDb.OrderByDescending(p => p.Name).ToList();
                    break;
                case "PriceAsc":
                    productsFromDb = productsFromDb.OrderBy(p => p.Value).ToList();
                    break;
                case "PriceDesc":
                    productsFromDb = productsFromDb.OrderByDescending(p => p.Value).ToList();
                    break;
                case "DateAsc":
                    productsFromDb = productsFromDb.OrderBy(p => p.CreationTime).ToList();
                    break;
                case "DateDesc":
                    productsFromDb = productsFromDb.OrderByDescending(p => p.CreationTime).ToList();
                    break;
                default:
                    
                    productsFromDb = productsFromDb.OrderBy(p => p.Name).ToList();
                    break;
            }

            ViewBag.Products = AllproductsFromDb;
            ViewBag.Selected = productsFromDb;
            ViewBag.SortOrder = sortOrder; // Przekazanie informacji o aktualnym sposobie sortowania do widoku
            return View("Index", productsFromDb); // Przekieruj do widoku Index.cshtml z wynikami zapytania
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
