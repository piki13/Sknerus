using HtmlAgilityPack;
using Skapiec.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testowy
{
    [TestClass]
     public class ScrapperServiceTest
    {
        [TestMethod]
        public void ShouldReturnValidDataWhenSearchQueryIsCorrect()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            StreamReader streamReader = new StreamReader("Test.html");
            var testData = streamReader.ReadToEnd();
            htmlDocument.LoadHtml(testData);
            var nodeToTest = htmlDocument.DocumentNode;
            var actualData = ScraperService.getSingleNode(nodeToTest, "/html/body/div/h1");
            Assert.AreEqual("TestHeader", actualData.InnerText);
            
        }

        [TestMethod]
        public void cos()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            StreamReader streamReader = new StreamReader("Test.html");
            var testData = streamReader.ReadToEnd();
            htmlDocument.LoadHtml(testData);
            var nodeToTest = htmlDocument.DocumentNode;
            var actualData = ScraperService.getSingleNode(nodeToTest, "/html/body/div/h1");
            Assert.ThrowsException<ArgumentException>(() => ScraperService.getSingleNode(nodeToTest, null));
        }

        public void ShouldReturnNullWhenElementNotFound()
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            StreamReader streamReader = new StreamReader("Test.html");
            var testData = streamReader.ReadToEnd();
            htmlDocument.LoadHtml(testData);
            var nodeToTest = htmlDocument.DocumentNode;
            var actualData = ScraperService.getSingleNode(nodeToTest, "/html/body/div/nonexistent_element");
            Assert.IsNull(actualData);
        }
        //Przetestować dostarczenie do metody nulla jako pierwszy parametr, drugi test dostarczenie nulla jako drugi parametr trzeci proba wyszukania czegos czego nie ma w dokumencie
    }
}
