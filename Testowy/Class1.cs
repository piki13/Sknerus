using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Skapiec.Controllers;
using Skapiec.Entities;
using Skapiec.Models;
using Skapiec.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Assert = Xunit.Assert;

namespace Skapiec.Tests
{
    public class ScraperServiceTests
    {
        [Fact]
        public async Task Search_ScrapesAndSavesProducts()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<SkapiecDBcontext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            var dbContext = new SkapiecDBcontext(dbContextOptions);

            var httpClientMock = new Mock<HttpClient>();
            httpClientMock.Setup(client => client.GetStringAsync(It.IsAny<string>())).ReturnsAsync(GetMockHtmlContent());

            var scraperService = new ScraperService(dbContext);

            var viewModel = new SearchViewModel { Name = "TestProduct" };

            // Act
            var result = await scraperService.Search(viewModel);

            // Assert
            Assert.IsType<ViewResult>(result); // Upewnij się, że metoda zwraca ViewResult

            var products = await dbContext.Products.ToListAsync();
            Assert.Equal(20, products.Count); // Upewnij się, że zapisano dokładnie 20 produktów

            foreach (var product in products)
            {
                Assert.Equal("TestProduct", product.query); // Upewnij się, że każdy produkt ma właściwe zapytanie
            }
        }

        private string GetMockHtmlContent()
        {
            // Symulowany HTML zawierający dane produktów do analizy
            return @"<html>
                        <body>
                            <div>
                                <main>
                                    <div>
                                        <div>
                                            <div>
                                                <a aria-label='Product 1' href='#'></a>
                                                <div>
                                                    <div>
                                                        <div>
                                                            <span>10 zł</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div>
                                                    <div>
                                                        <div>
                                                            <img src='image1.jpg'>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <!-- More product elements -->
                                    </div>
                                </main>
                            </div>
                        </body>
                    </html>";
        }
    }
}
