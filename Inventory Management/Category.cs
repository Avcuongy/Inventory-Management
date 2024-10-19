using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory_Management
{
    internal class Category : ISerializable
    {
        private string categoryName;
        private List<Product> products;
        public string CategoryName { get => categoryName; set => categoryName = value; }
        public List<Product> Products { get => products; set => products = value; }

        public Category() { }

        public Category(string categoryName)
        {
            this.categoryName = categoryName;
            this.products = new List<Product>();
        }

        public Category(SerializationInfo info, StreamingContext context)
        {
            CategoryName = info.GetString("CategoryName");
            Products = (List<Product>)info.GetValue("Products", typeof(List<Product>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CategoryName", CategoryName);
            info.AddValue("Products", Products);
        }
    }
}