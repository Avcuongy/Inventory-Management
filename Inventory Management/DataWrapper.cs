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
    public class DataWrapper
    {
        private Warehouse _warehouse;
        private List<Supplier> _suppliers;
        private List<PurchaseOrder> _purchaseOrders;
        private List<ReturnOrder> _returnOrders;
        private List<Customer> _customers;
        private OrderManager _orderManager;
        private List<SalesInvoice> _salesInvoices;
        private Report _report;

        public Warehouse Warehouse { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<ReturnOrder> ReturnOrders { get; set; }
        public List<Customer> Customers { get; set; }
        public OrderManager OrderManager { get; set; }
        public List<SalesInvoice> SalesInvoices { get; set; }
        public Report Report { get; set; }

        public DataWrapper()
        {
            Warehouse = new Warehouse();
            Suppliers = new List<Supplier>();
            PurchaseOrders = new List<PurchaseOrder>();
            ReturnOrders = new List<ReturnOrder>();
            Customers = new List<Customer>();
            OrderManager = new OrderManager(new List<PurchaseOrder>());
            SalesInvoices = new List<SalesInvoice>();
            Report = new Report(new List<SalesInvoice>());
        }
    }
}

