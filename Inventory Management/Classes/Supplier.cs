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
    public class Supplier : ISerializable
    {
        private string supplierId;
        private string name;
        private string contactInfo;
        private List<Product> suppliedProducts = new List<Product>();
        public string SupplierId { get => supplierId; set => supplierId = value; }
        public string Name { get => name; set => name = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }
        public List<Product> SuppliedProducts { get => suppliedProducts; set => suppliedProducts = value; }

        public Supplier() { }

        public Supplier(SerializationInfo info, StreamingContext context)
        {
            SupplierId = info.GetString("SupplierId");
            Name = info.GetString("Name");
            ContactInfo = info.GetString("ContactInfo");
            SuppliedProducts = (List<Product>)info.GetValue("SuppliedProducts", typeof(List<Product>));
        }

        public Supplier(string supplierId, string name, string contactInfo, List<Product> suppliedProducts)
        {
            this.supplierId = supplierId;
            this.name = name;
            this.contactInfo = contactInfo;
            this.suppliedProducts = suppliedProducts;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("SupplierId", SupplierId);
            info.AddValue("Name", Name);
            info.AddValue("ContactInfo", ContactInfo);
            info.AddValue("SuppliedProducts", SuppliedProducts);
        }
    }
}
