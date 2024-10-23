using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Drawing;
using System.IO;

namespace Inventory_Management
{
    public abstract class Product : ISerializable
    {
        private string productId;
        private string name;
        private string category;
        private int quantity;
        private double price;
        public string ProductId { get => productId; set => productId = value; }
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
        public string Category { get => category; set => category = value; }

        public Product() { }
        public Product(SerializationInfo info, StreamingContext context)
        {
            ProductId = info.GetString("ID");
            Name = info.GetString("Name");
            Category = (string)info.GetValue("Category", typeof(string));
            Quantity = info.GetInt32("Quantity");
            Price = info.GetDouble("Price");
        }  
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ProductId);
            info.AddValue("Name", Name);
            info.AddValue("Category", Category, typeof(string));
            info.AddValue("Quantity", Quantity);
            info.AddValue("Price", Price);
        } 
        public Product(string productId, string name, string category, int quantity, double price)
        {
            this.productId = productId;
            this.name = name;
            this.category = category;
            this.quantity = quantity;
            this.price = price;
        }
    }
}
