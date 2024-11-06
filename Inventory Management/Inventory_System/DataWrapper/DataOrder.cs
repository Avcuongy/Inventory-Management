using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory_Management.Inventory_System
{
    public class DataOrder
    {
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<ReturnOrder> ReturnOrders { get; set; }
        public OrderManager OrderManager { get; set; }
        public DataOrder(List<PurchaseOrder> purchaseOrders, List<ReturnOrder> returnOrders, OrderManager orderManager)
        {
            PurchaseOrders = purchaseOrders;
            ReturnOrders = returnOrders;
            OrderManager = orderManager;
        }
        public DataOrder() 
        {
            
        }
    }
}
