using HtmlAgilityPack;
using System;
using Microsoft.AspNetCore.Mvc;
using Skapiec.Entities;
using Skapiec.Models;

namespace Skapiec.Controllers
{
    public class ScrapController : Controller
    {
        private readonly SkapiecDBcontext dBcontext;

        public ScrapController(SkapiecDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
        }


        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel viewModel)
        {
            using (HttpClient client = new HttpClient())
            {
               //1 solution
               /*
               //Custom query (working :) )
               string url = $"https://www.skapiec.pl/szukaj?query=.{viewModel.name}";

               //Get page content
               string htmlContent = await client.GetStringAsync(url);

               //Create HTML object
               HtmlDocument htmlDocument = new HtmlDocument();
               htmlDocument.LoadHtml(htmlContent);

               //scraping
               var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
               if (links != null)
               {
                   foreach (var link in links)
                   {
                       Console.WriteLine(link.Attributes["href"].Value);
                   }
               }
               */

                //new solution
                
                string url = $"https://www.skapiec.pl/szukaj?query=.{viewModel.name}";
                var web = new HtmlWeb();
                var document = web.Load(url);

                var results = document.QuerySelectorAll("a");

                foreach (var result in results)
                {
                    Console.WriteLine(result.Attributes["href"].Value);
                }
            }

            var product = new Product
            {
                name = viewModel.name,
                value = 20,
                link = "test"
            };

            await dBcontext.Products.AddAsync(product);
            //await dBcontext.SaveChangesAsync();

            return View();
        }

    }
}



    /*[HttpPost]
    public async Task<IActionResult> Index(string searchPhrase)
    {
        // Tutaj możesz użyć searchPhrase do przeprowadzenia wyszukiwania
        // na stronie internetowej za pomocą scrapera
        var results = await _webScraper.SearchAndScrape(searchPhrase);

        // Tutaj możesz przetworzyć otrzymane wyniki i dodać je do bazy danych

        return View(results); // Możesz zwrócić widok z wynikami wyszukiwania
    }
}
    */

