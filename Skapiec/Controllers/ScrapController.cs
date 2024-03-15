using HtmlAgilityPack;
using System;
using Microsoft.AspNetCore.Mvc;
using Skapiec.Entities;
using Skapiec.Models;
using Skapiec.Services;

namespace Skapiec.Controllers
{
    public class ScrapController : Controller
    {
        private readonly ScraperService _scraperService;

        public ScrapController(ScraperService scraperService)
        {
            _scraperService = scraperService;
        }


        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel viewModel)
        {
            return await _scraperService.Search(viewModel);
        }

    }
}
