using Newtonsoft.Json;
using NUnit.Framework;
using ShopifyWatcher;
using System;

namespace ShopifyWatcherTests
{
    [TestFixture]
    public class ShopifyWatcherTests
    {
        public ShopifyWatcherTests()
        {

        }

        public object SiteMap { get; private set; }

        [Test]
        public void TestParseProductJSON()
        {
            Product product;
            IProductService service = new ProductService();
            product = service.GetProduct("water-bottle.json");
            Assert.IsNotNull(product);
        }

        [Test]
        public void TestParseSiteMap()
        {
            UrlSet siteMap;
            SiteMapService service = new SiteMapService();
            siteMap = service.GetSiteMap("sitemap_products_1.xml");
            Assert.AreEqual(30, siteMap.Urls.Count);
        }

    }
}
