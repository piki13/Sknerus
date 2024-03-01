using Microsoft.AspNetCore.Mvc;

namespace Skapiec.Controllers
{
    public class ScrapController : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

    }
}


