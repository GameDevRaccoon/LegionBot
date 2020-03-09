using System;
using System.Collections.Generic;
using System.Text;

namespace ShopifyWatcher
{
    public enum InventoryPolicy { Continue, Deny};
    public class ProductContainer
    {
        public Product Product { get; set; }
    }
    public class Product
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public String Vendor { get; set; }
        public string Product_Type { get; set; }
        public DateTime Created_At { get; set; }
        public string Handle { get; set; }
        public DateTime Updated_At { get; set; }
        public DateTime Published_At { get; set; }
        public string Template_Suffix { get; set; }
        public string Tags { get; set; }
        public string Published_Scope { get; set; }
        public List<Variant> Variants { get; set; }
    }
    public class Variant
    {
        public long ID { get; set; }
        public long Product_ID { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Sku { get; set; }
        public int Position { get; set; }
        public InventoryPolicy Inventory_Policy { get; set; }
        //"compare_at_price": null,
        public int? Compare_At_Price { get; set; }
        //"fulfillment_service": "manual",
        public string Fulfillment_Service { get; set; }
        //"inventory_management": "shopify",
        public string Inventory_Management { get; set; }
        //"option1": "Default Title",

        //"option2": null,
        //"option3": null,
        public DateTime Created_At { get; set; }
        //"created_at": "2018-02-22T15:44:53-08:00",
        public DateTime Updated_At{get;set;}
        //"updated_at": "2018-07-17T11:09:17-07:00",
        ///"taxable": true,
        public bool Taxable { get; set; }
        //"barcode": "",
        ///"grams": 454,
        public int Grams { get; set; }
        //"image_id": null,

        //"inventory_quantity": -17,
        public int Inventory_Quantity { get; set; }
        //"weight": 16.0144,
        public decimal Weight { get; set; }
        //"weight_unit": "oz",
        // Move to a enum
        public string Weight_Unit { get; set; }
        //"inventory_item_id": 1917418340368,
        public long Inventory_Item_ID { get; set; }
        //"old_inventory_quantity": -17,
        public int Old_Inventory_Quantity { get; set; }
        //"requires_shipping": true
        public bool Requires_Shipping { get; set; }
    }
}
