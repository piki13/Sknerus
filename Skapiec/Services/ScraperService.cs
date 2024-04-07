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


        public static HtmlNode getProducts (HtmlDocument htmlDocument)
        {
            return htmlDocument.DocumentNode.SelectSingleNode("/html/body/div/main/div[2]/div[3]");
        }
        
        public static HtmlNode getSingleNode(HtmlNode htmlNode, string nodePath)
        {
            return htmlNode.SelectSingleNode(nodePath);
        }
        //[HttpPost]
        public async Task<IActionResult> Search(SearchViewModel viewModel)
        {
            using (HttpClient client = new HttpClient())
            {

                string url = $"https://www.skapiec.pl/szukaj?query={viewModel.Name}";
                string htmlContent = await client.GetStringAsync(url);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                var products = getProducts(htmlDocument);

                var productsFromDb = await dBcontext.Products.ToListAsync();

                for (int i = 1; i <= 20; i++)
                {
                    var processedProduct = getSingleNode(products, "div[" + i + "]");
                    var porcessedProductName = getSingleNode(processedProduct, "div/a").Attributes["Aria-label"].Value;
                    var processedProductPrice = getSingleNode(processedProduct, "div/div/div[3]/span[1]").InnerText;
                    var processeProductLink = getSingleNode(processedProduct, "div/a").Attributes["href"].Value;
                    var processedImgUrl = getSingleNode(processedProduct, "div/div/div/div/img").Attributes["src"].Value;
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

            var resultsFromDb = await dBcontext.Products
                .Where(p => p.query == viewModel.Name)
                .ToListAsync();
            ViewBag.Products = resultsFromDb;

            return View();
        }
    }
}
