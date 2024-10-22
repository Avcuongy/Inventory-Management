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
        private int productId;
        private string name;
        private string category;
        private int quantity;
        private double price;
        private Bitmap productImage;
        public int ProductId { get => productId; set => productId = value; }
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
        public string Category { get => category; set => category = value; }
        public Bitmap ProductImage { get => productImage; set => productImage = value; }

        public Product() { }
        public Product(SerializationInfo info, StreamingContext context)
        {
            ProductId = info.GetInt32("ID");
            Name = info.GetString("Name");
            Category = (string)info.GetValue("Category", typeof(string));
            Quantity = info.GetInt32("Quantity");
            Price = info.GetDouble("Price");
            ProductImage = (Bitmap)info.GetValue("ProductImage", typeof(Bitmap));
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ProductId);
            info.AddValue("Name", Name);
            info.AddValue("Category", Category, typeof(string));
            info.AddValue("Quantity", Quantity);
            info.AddValue("Price", Price);
            info.AddValue("ProductImage",ProductImage,typeof(Bitmap));
        }
    }
}
