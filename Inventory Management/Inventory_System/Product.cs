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
    [JsonDerivedType(typeof(Phone), typeDiscriminator: "Phone")]
    [JsonDerivedType(typeof(Tablet), typeDiscriminator: "Tablet")]
    [JsonDerivedType(typeof(Keyboard), typeDiscriminator: "Keyboard")]
    [JsonDerivedType(typeof(Headphone), typeDiscriminator: "Headphone")]
    [JsonDerivedType(typeof(Mouse), typeDiscriminator: "Mouse")]
    public abstract class Product : ISerializable, ICloneable
    {
        private string productId;
        private string name;
        private string category;
        private int quantity;
        private double price;
        public string ProductId { get => productId; set => productId = value; }
        public string Name { get => name; set => name = value; }
        public string Category { get => category; set => category = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public double Price { get => price; set => price = value; }

        [JsonConstructor]
        public Product(string productId, string name, string category, int quantity, double price)
        {
            this.productId = productId;
            this.name = name;
            this.category = category;
            this.quantity = quantity;
            this.price = price;
        }
        public Product(SerializationInfo info, StreamingContext context)
        {
            ProductId = info.GetString("ID");
            Name = info.GetString("Name");
            Category = info.GetString("Category");
            Quantity = info.GetInt32("Quantity");
            Price = info.GetDouble("Price");
        }
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ProductId);
            info.AddValue("Name", Name);
            info.AddValue("Category", Category);
            info.AddValue("Quantity", Quantity);
            info.AddValue("Price", Price);
        }
        public Product() { }
        public abstract object Clone();
    }
}
