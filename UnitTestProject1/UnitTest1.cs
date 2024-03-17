using Skapiec.Entities;
using System;
using Xunit;

namespace Skapiec.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_PropertiesAreSetCorrectly()
        {
            // Arrange
            var product = new Product
            {
                Name = "TestProduct",
                Value = 10.99,
                Link = "https://example.com",
                ImgUrl = "https://example.com/image.jpg",
                CreationTime = new DateTime(2024, 3, 17),
                query = "TestQuery"
            };

            // Act - No action needed for this test

            // Assert
            Assert.Equal("TestProduct", product.Name);
            Assert.Equal(10.99, product.Value);
            Assert.Equal("https://example.com", product.Link);
            Assert.Equal("https://example.com/image.jpg", product.ImgUrl);
            Assert.Equal(new DateTime(2024, 3, 17), product.CreationTime);
            Assert.Equal("TestQuery", product.query);
        }
    }
}