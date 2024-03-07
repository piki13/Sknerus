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
                var results = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
                if (results != null)
                {
                    foreach (var result in results)
                    {
                        //Console.WriteLine(result.Attributes["href"].Value);
                    }
                }*/
               

                //2 solution
                /*
                string url = $"https://www.skapiec.pl/szukaj?query=.{viewModel.name}";
                var web = new HtmlWeb();
                var document = web.Load(url);

                var results = document.QuerySelectorAll("a");
                foreach (var result in results)
                {
                    //Console.WriteLine(result.Attributes["href"].Value);
                    Console.WriteLine(result.Attributes["href"].Value);
                }*/

                //3 solution
                
                string url = $"https://www.skapiec.pl/szukaj?query={viewModel.name}";
                string htmlContent = await client.GetStringAsync(url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var products = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div/main/div[2]/div[3]");

                for (int i = 1; i <= 20; i++) { 
                    var processedProduct = products.SelectSingleNode("div[" + i + "]");
                    var porcessedProductName = processedProduct.SelectSingleNode("div/a").Attributes["Aria-label"].Value;
                    var processedProductPrice = processedProduct.SelectSingleNode("div/div/div[3]/span[1]").InnerText;
                    var processeProdictLink = processedProduct.SelectSingleNode("div/a").Attributes["href"].Value;


                    char[] totrim = {'z', 'ł', ','};

                    double converterdprice = Convert.ToDouble(processedProductPrice.Trim(totrim));
                    Console.WriteLine(converterdprice + "\n");
                    var product = new Product
                    {
                        name = porcessedProductName,
                        value = converterdprice,
                        link = processeProdictLink
                    };

                    await dBcontext.Products.AddAsync(product);
                    await dBcontext.SaveChangesAsync();
                }
            }



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

