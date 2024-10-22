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
    public class OrderManager : ISerializable
    {
        private List<PurchaseOrder> orders = new List<PurchaseOrder>();
        public List<PurchaseOrder> Orders { get => orders; set => orders = value; }
        public void CreateOrder(SerializationInfo info, StreamingContext context)
        {
            Orders = (List<PurchaseOrder>)info.GetValue("Orders", typeof(List<PurchaseOrder>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Orders", Orders, typeof(PurchaseOrder));
        }
        public OrderManager() { }
        public OrderManager(List<PurchaseOrder> orders)
        {
            this.orders = orders;
        }
    }
}
