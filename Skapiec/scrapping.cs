using System;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Skapiec
{
    public class Scrapping
    {
        static async Task Main()
        {
            // Przykładowy URL do scrapingu
            string url = "https://www.skapiec.pl/szukaj?query=rower&categoryId=";

            // Utwórz klienta HTTP
            using (HttpClient client = new HttpClient())
            {
                // Pobierz zawartość strony
                string htmlContent = await client.GetStringAsync(url);

                // Utwórz obiekt HtmlDocument z zawartości strony
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(htmlContent);

                // Przykład: Znajdź wszystkie linki na stronie
                var links = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
                if (links != null)
                {
                    foreach (var link in links)
                    {
                        Console.WriteLine(link.Attributes["href"].Value);
                    }
                }
            }
        }
    }
}
