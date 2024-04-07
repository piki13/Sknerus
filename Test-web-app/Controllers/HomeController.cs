using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test_web_app.Models;

namespace Test_web_app.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
