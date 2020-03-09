using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyWatcher
{
    public interface IProductService
    {
        Product GetProduct(string productJSON);
    }

    public class ProductService : IProductService
    {
        public Product GetProduct(string productJSON)
        {
            Product product;
            using (System.IO.StreamReader r = new System.IO.StreamReader(productJSON))
            {
                string json = r.ReadToEnd();
                product = JsonConvert.DeserializeObject<ProductContainer>(json).Product;
            }
            return product;
        }
    }
}
