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



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var AllproductsFromDb = await dBcontext.Products.ToListAsync();

            ViewBag.Products = AllproductsFromDb;
            return View(); // Przekieruj do widoku QueryResults.cshtml
        }


        [HttpPost]
        public async Task<IActionResult> Index(string queryColumn)
        {
            var AllproductsFromDb = await dBcontext.Products.ToListAsync();
            var productsFromDb = await dBcontext.Products
                                                .Where(p => p.query == queryColumn)
                                                .ToListAsync();

            //var productsFromDb = await dBcontext.Products.ToListAsync();

            ViewBag.Products = AllproductsFromDb;
            ViewBag.Selected = productsFromDb;
            return View("Index", productsFromDb); // Przekieruj do widoku Index.cshtml z wynikami zapytania
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
