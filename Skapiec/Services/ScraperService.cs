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
        private int _counter = 0;

        public ScraperService(SkapiecDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
        }

        //[HttpPost]
        public async Task<IActionResult> Search(SearchViewModel viewModel)
        {
            using (HttpClient client = new HttpClient())
            {
                //https://www.skapiec.pl/szukaj?query=rower&page=2
                string url = $"https://www.skapiec.pl/szukaj?query={viewModel.Name}&page={viewModel.PageNumber}";
                string htmlContent = await client.GetStringAsync(url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var products = htmlDocument.DocumentNode.SelectSingleNode("/html/body/div/main/div[2]/div[3]");

                var productsFromDb = await dBcontext.Products.ToListAsync();

                if (products != null && viewModel.Name != null)
                {
                    for (int i = 1; i <= 20; i++)
                    {
                        var processedProduct = products.SelectSingleNode("div[" + i + "]");
                        if (processedProduct == null)
                        {
                            break;
                        }
                        string porcessedProductName = processedProduct.SelectSingleNode("div/a").Attributes["Aria-label"].Value;
                        string processedProductPrice = processedProduct.SelectSingleNode("div/div/div[3]/span[1]").InnerText;
                        string processedProductLink = processedProduct.SelectSingleNode("div/a").Attributes["href"].Value;
                        string processedImgUrl = processedProduct.SelectSingleNode("div/div/div/div/img").Attributes["src"].Value;
                        string querytoprocess = viewModel.Name;

                        if (processedProductLink.StartsWith('/'))
                        {
                            processedProductLink = "https://www.skapiec.pl" + processedProductLink;
                        }


                        char[] totrim = { 'z', 'ł' };
                        double converterdprice = Convert.ToDouble(processedProductPrice.Trim(totrim));

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
                            Link = processedProductLink,
                            ImgUrl = processedImgUrl,
                            CreationTime = DateTime.Now,
                            query = querytoprocess
                        };

                        await dBcontext.Products.AddAsync(product);
                        await dBcontext.SaveChangesAsync();


                    }
                    var resultsFromDb = await dBcontext.Products
                        .Where(p => p.query == viewModel.Name).ToListAsync();
                    ViewBag.Products = resultsFromDb;
                    _counter = viewModel.PageNumber + 1;
                }
                else
                {
                    return View("Error");
                }

            }
            var myModel = new SearchViewModel
            {
                Name = viewModel.Name,
                PageNumber = _counter
            };


            return View(myModel);
        }
    }
}
