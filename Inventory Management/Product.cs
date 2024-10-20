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

namespace Inventory_Management
{
    public abstract class Product : ISerializable
    {
        private int productId;
        private string name;
        private Category category;
        private int quantity;
        private double price;
        private Bitmap productImage;

        public int ProductId { get => productId; set => productId = value; }
        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }
        Category Category { get => category; set => category = value; }
        public Bitmap ProductImage { get => productImage; set => productImage = value; }

        public abstract void AddProduct();
        public abstract void UpdateProduct();

        public Product() { }
        public Product(SerializationInfo info, StreamingContext context)
        {
            ProductId = info.GetInt32("ID");
            Name = info.GetString("Name");
            Category = (Category)info.GetValue("Category", typeof(Category));
            Quantity = info.GetInt32("Quantity");
            Price = info.GetDouble("Price");
            ProductImage = (Bitmap)info.GetValue("ProductImage", typeof(Bitmap));
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ProductId);
            info.AddValue("Name", Name);
            info.AddValue("Category", Category, typeof(Category));
            info.AddValue("Quantity", Quantity);
            info.AddValue("Price", Price);
            info.AddValue("ProductImgage",typeof(Bitmap));
        }
    }
}
