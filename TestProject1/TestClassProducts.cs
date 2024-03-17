using Skapiec.Entities;

namespace TestProject1
{
    [TestClass]
    public class TestClassProducts
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            var product = new Product
            {
                Name = "TestProduct",
                Value = 10.99,
                Link = "https://www.skapiec.pl/cat/2215-rowery.html",
                ImgUrl = "https://www.skapiec.pl/site/cat/2215/comp/921873507",
                CreationTime = new DateTime(2024, 3, 17),
                query = "Rower"
            };

            // Act

            // Assert
            Assert.AreEqual("TestProduct", product.Name);
            Assert.AreEqual(10.99, product.Value);
            Assert.AreEqual("https://www.skapiec.pl/cat/2215-rowery.html", product.Link);
            Assert.AreEqual("https://www.skapiec.pl/site/cat/2215/comp/921873507", product.ImgUrl);
            Assert.AreEqual(new DateTime(2024, 3, 17), product.CreationTime);
            Assert.AreEqual("Rower", product.query);
        }
    }
}