using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Skapiec.Entities;
using Skapiec.Models;
using Skapiec.Controllers;

namespace Skapiec.Services
{
    public class ScraperService : Controller
    {

        private readonly SkapiecDBcontext dBcontext;

        public ScraperService(SkapiecDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
        }

        //[HttpPost]
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

                string url = $"https://www.skapiec.pl/szukaj?query={viewModel.Name}";
                string htmlContent = await client.GetStringAsync(url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var products = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div/main/div[2]/div[3]");

                var productsFromDb = await dBcontext.Products.ToListAsync();

                for (int i = 1; i <= 20; i++)
                {
                    var processedProduct = products.SelectSingleNode("div[" + i + "]");
                    var porcessedProductName = processedProduct.SelectSingleNode("div/a").Attributes["Aria-label"].Value;
                    var processedProductPrice = processedProduct.SelectSingleNode("div/div/div[3]/span[1]").InnerText;
                    var processeProductLink = processedProduct.SelectSingleNode("div/a").Attributes["href"].Value;
                    var processedImgUrl = processedProduct.SelectSingleNode("div/div/div/div/img").Attributes["src"].Value;
                    var querytoprocess = viewModel.Name;


                    char[] totrim = { 'z', 'ł' };

                    double converterdprice = Convert.ToDouble(processedProductPrice.Trim(totrim));
                    Console.WriteLine(processedImgUrl + " ");
                    Console.WriteLine(converterdprice + "\n");

                    foreach (var pro in productsFromDb)
                    {
                        if (pro.Name == porcessedProductName)
                        {
                            dBcontext.Products.Remove(pro);
                        }
                    }
                
                        var product = new Product
                        {
                            Name = porcessedProductName,
                            Value = converterdprice,
                            Link = processeProductLink,
                            ImgUrl = processedImgUrl,
                            CreationTime = DateTime.Now,
                            query = querytoprocess
                        };

                        await dBcontext.Products.AddAsync(product);
                        await dBcontext.SaveChangesAsync();
                    
                    
                }
            }
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
