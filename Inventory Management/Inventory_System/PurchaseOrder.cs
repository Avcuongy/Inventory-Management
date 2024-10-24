using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory_Management
{
    public class PurchaseOrder : ISerializable
    {
        private string orderId;
        private Supplier supplier;
        private string status;
        private List<Product> orderedProducts = new List<Product>();

        public PurchaseOrder() { }

        public PurchaseOrder(SerializationInfo info, StreamingContext context)
        {
            OrderId = info.GetString("OrderId");
            Supplier = (Supplier)info.GetValue("Supplier", typeof(Supplier));
            Status = info.GetString("Status");
            OrderedProducts = (List<Product>)info.GetValue("OrderedProducts", typeof(List<Product>));
        }
        public PurchaseOrder(string orderId, Supplier supplier, string status, List<Product> orderedProducts)
        {
            this.OrderId = orderId;
            this.Supplier = supplier;
            this.Status = status;
            this.OrderedProducts = orderedProducts;
        }

        public string OrderId { get => orderId; set => orderId = value; }
        public Supplier Supplier { get => supplier; set => supplier = value; }
        public string Status { get => status; set => status = value; }
        public List<Product> OrderedProducts { get => orderedProducts; set => orderedProducts = value; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("OrderId", OrderId);
            info.AddValue("Supplier", Supplier, typeof(Supplier));
            info.AddValue("Status", Status);
            info.AddValue("OrderedProducts", OrderedProducts, typeof(List<Product>));

        }
    }
}
