using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Inventory_Management
{
    public delegate void OrderChangeInformation();
    public partial class Order_Menu : Form
    {
        private string _username;
        private Warehouse _warehouse;
        private List<Supplier> _supplier = new List<Supplier>();
        private List<PurchaseOrder> _purchaseOrder = new List<PurchaseOrder>();
        private List<ReturnOrder> _returnOrder = new List<ReturnOrder>();
        private List<Customer> _customer = new List<Customer>();
        private OrderManager _orderManager;
        private List<SalesInvoice> _salesInvoice = new List<SalesInvoice>();
        private Report _report;
        public Order_Menu(string username,
                        Warehouse warehouse,
                        List<Supplier> supplier,
                        List<PurchaseOrder> purchaseOrder,
                        List<ReturnOrder> returnOrder,
                        List<Customer> customer,
                        OrderManager orderManager,
                        List<SalesInvoice> salesInvoice,
                        Report report)
        {
            InitializeComponent();
            Username = username;
            Warehouse = warehouse;
            Supplier = supplier;
            PurchaseOrder = purchaseOrder;
            ReturnOrder = returnOrder;
            Customer = customer;
            OrderManager = orderManager;
            SalesInvoice = salesInvoice;
            Report = report;
            ShowOrdersInfo();
        }
        private bool ChangeDataView = false;
        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }
        public void ShowOrdersInfo()
        {
            ShowOrder.CurrentCell = null;

            List<Supplier> suppliers = Supplier;
            OrderManager orderManager = OrderManager;
            List<PurchaseOrder> purchaseOrders = OrderManager.Orders;
            List<Product> products = Warehouse.Products;

            DataTable dt = new DataTable();

            dt.Columns.Add("Order ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Quantity Order");
            dt.Columns.Add("Total");
            dt.Columns.Add("Status");

            foreach (PurchaseOrder order in purchaseOrders)
            {
                string supplierName = order.Supplier.Name;

                foreach (Product product in order.OrderedProducts)
                {
                    DataRow row = dt.NewRow();
                    row["Order ID"] = order.OrderId;
                    row["Supplier Name"] = supplierName;
                    row["Product ID"] = product.ProductId;
                    row["Product Name"] = product.Name;
                    row["Quantity Order"] = product.Quantity;
                    foreach (Product product2 in products)
                    {
                        if (product.ProductId == product2.ProductId)
                        {
                            row["Total"] = product2.Price * product.Quantity;
                            break;
                        }
                    }
                    row["Status"] = order.Status;

                    dt.Rows.Add(row);
                }
            }

            ShowOrder.DataSource = dt;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile(
                                   Username,
                                   Warehouse,
                                   Supplier,
                                   PurchaseOrder,
                                   ReturnOrder,
                                   Customer,
                                   OrderManager,
                                   SalesInvoice,
                                   Report
               );
            profile.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowOrder.CurrentCell = null;

            List<Supplier> suppliers = Supplier;
            OrderManager orderManager = OrderManager;
            List<PurchaseOrder> purchaseOrders = OrderManager.Orders;
            List<Product> products = Warehouse.Products;

            string id = textBoxSupplier.Text.Trim();

            DataTable dt = new DataTable();

            dt.Columns.Add("Order ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Quantity Order");
            dt.Columns.Add("Total");
            dt.Columns.Add("Status");

            if (string.IsNullOrEmpty(id))
            {
                foreach (PurchaseOrder order in purchaseOrders)
                {
                    string supplierName = order.Supplier.Name;

                    foreach (Product product in order.OrderedProducts)
                    {
                        DataRow row = dt.NewRow();
                        row["Order ID"] = order.OrderId;
                        row["Supplier Name"] = supplierName;
                        row["Product ID"] = product.ProductId;
                        row["Product Name"] = product.Name;
                        row["Quantity Order"] = product.Quantity;

                        foreach (Product product2 in products)
                        {
                            if (product.ProductId == product2.ProductId)
                            {
                                row["Total"] = product2.Price * product.Quantity;
                                break;
                            }
                        }

                        row["Status"] = order.Status;

                        dt.Rows.Add(row);
                    }
                }
            }
            else
            {
                foreach (PurchaseOrder order in purchaseOrders)
                {
                    if (order.OrderId == id || order.Supplier.SupplierId == id || order.Supplier.Name == id)
                    {
                        string supplierName = order.Supplier.Name;

                        foreach (Product product in order.OrderedProducts)
                        {
                            DataRow row = dt.NewRow();
                            row["Order ID"] = order.OrderId;
                            row["Supplier Name"] = supplierName;
                            row["Product ID"] = product.ProductId;
                            row["Product Name"] = product.Name;
                            row["Quantity Order"] = product.Quantity;

                            foreach (Product product2 in products)
                            {
                                if (product.ProductId == product2.ProductId)
                                {
                                    row["Total"] = product2.Price * product.Quantity;
                                    break;
                                }
                            }

                            row["Status"] = order.Status;

                            dt.Rows.Add(row);
                        }
                        break;
                    }
                }
            }

            ShowOrder.DataSource = dt;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<Supplier> suppliers = Supplier;
            OrderManager orderManager = OrderManager;
            List<PurchaseOrder> purchaseOrders = orderManager.Orders;
            List<Product> products = Warehouse.Products;

            DataTable dt = new DataTable();

            if (ChangeDataView)
            {
                ShowOrder.CurrentCell = null;

                textBoxSupplier.Text = "Order ID";
                label1.Text = "Order";
                button4.Text = "Order"; 

                dt.Columns.Add("Order ID");
                dt.Columns.Add("Supplier Name");
                dt.Columns.Add("Product ID");
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Quantity Order");
                dt.Columns.Add("Total");
                dt.Columns.Add("Status");

                foreach (PurchaseOrder order in purchaseOrders)
                {
                    string supplierName = order.Supplier.Name;

                    foreach (Product product in order.OrderedProducts)
                    {
                        DataRow row = dt.NewRow();
                        row["Order ID"] = order.OrderId;
                        row["Supplier Name"] = supplierName;
                        row["Product ID"] = product.ProductId;
                        row["Product Name"] = product.Name;
                        row["Quantity Order"] = product.Quantity;

                        foreach (Product product2 in products)
                        {
                            if (product.ProductId == product2.ProductId)
                            {
                                row["Total"] = product2.Price * product.Quantity;
                                break;
                            }
                        }
                        row["Status"] = order.Status;

                        dt.Rows.Add(row);
                    }
                }
            }
            else
            {
                ShowOrder.CurrentCell = null;

                textBoxSupplier.Text = "Supplier ID";
                label1.Text = "Supplier";
                button4.Text = "Supplier";

                dt.Columns.Add("Supplier ID");
                dt.Columns.Add("Supplier Name");
                dt.Columns.Add("Contact Info");
                dt.Columns.Add("Product Supply");
                dt.Columns.Add("Category");
                dt.Columns.Add("Base Price");

                foreach (Supplier sup in suppliers)
                {
                    foreach (Product suppliedProduct in sup.SuppliedProducts)
                    {
                        DataRow row = dt.NewRow();
                        row["Supplier ID"] = sup.SupplierId;
                        row["Supplier Name"] = sup.Name;
                        row["Contact Info"] = sup.ContactInfo;
                        row["Product Supply"] = suppliedProduct.Name;
                        row["Category"] = suppliedProduct.Category;
                        row["Base Price"] = suppliedProduct.Price;
                        dt.Rows.Add(row);
                    }
                }
            }

            ShowOrder.DataSource = dt;

            ChangeDataView = !ChangeDataView; 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Order_Add order_Add = new Order_Add(
                                   Username,
                                   Warehouse,
                                   Supplier,
                                   PurchaseOrder,
                                   ReturnOrder,
                                   Customer,
                                   OrderManager,
                                   SalesInvoice,
                                   Report
               );
            order_Add.OrderChangeAdd += ShowOrdersInfo;
            order_Add.ShowDialog();
        }
        public void ShowAddOrder()
        {
            ShowOrdersInfo();
        }
    }
}
