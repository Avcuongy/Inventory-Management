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
        private int orderId;
        private Supplier supplier;
        private string status;
        private List<Product> orderedProducts = new List<Product>();

        public int OrderId { get => orderId; set => orderId = value; }
        public string Status { get => status; set => status = value; }
        public List<Product> OrderedProducts { get => orderedProducts; set => orderedProducts = value; }
        internal Supplier Supplier { get => supplier; set => supplier = value; }

        public PurchaseOrder(int id, Supplier supplier, List<Product> products)
        {
            this.orderId = id;
            this.supplier = supplier;
            this.orderedProducts = products;
        }

        public PurchaseOrder() { }

        public PurchaseOrder(SerializationInfo info, StreamingContext context)
        {
            OrderId = info.GetInt32("ID");
            Supplier = (Supplier)info.GetValue("Supplier", typeof(Supplier));
            Status = info.GetString("Status");
            OrderedProducts = (List<Product>)info.GetValue("OrderProducts", typeof(List<Product>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", OrderId);
            info.AddValue("Supplier", Supplier, typeof(Supplier));
            info.AddValue("Status", Status);
            info.AddValue("OrderProducts", OrderedProducts, typeof(List<Product>));

        }
    }
}
