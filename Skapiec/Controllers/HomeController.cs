using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skapiec.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skapiec.Controllers
{
    public class HomeController : Controller
    {
        private readonly SkapiecDBcontext _dbcontext;

        public HomeController(SkapiecDBcontext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IActionResult Index(string query, string sortBy)
        {
            IQueryable<Product> productsQuery = _dbcontext.Products;

            if (!string.IsNullOrEmpty(query))
            {
                productsQuery = productsQuery.Where(p => p.query == query);
            }

            switch (sortBy)
            {
                case "Name_ASC":
                    productsQuery = productsQuery.OrderBy(p => p.Name);
                    break;
                case "Name_DESC":
                    productsQuery = productsQuery.OrderByDescending(p => p.Name);
                    break;
                case "Value_ASC":
                    productsQuery = productsQuery.OrderBy(p => p.Value);
                    break;
                case "Value_DESC":
                    productsQuery = productsQuery.OrderByDescending(p => p.Value);
                    break;
                case "CreationTime_ASC":
                    productsQuery = productsQuery.OrderBy(p => p.CreationTime);
                    break;
                case "CreationTime_DESC":
                    productsQuery = productsQuery.OrderByDescending(p => p.CreationTime);
                    break;
                default:
                    productsQuery = productsQuery.OrderBy(p => p.Id);
                    break;
            }

            var products = productsQuery.ToList();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_ProductTablePartial", products);
            }
            else
            {
                return View(products);
            }
        }
    }
}
