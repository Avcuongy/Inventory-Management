using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory_Management
{
    public partial class Profile : Form
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
        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

        public Profile(string username,
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
            ShowInfoInProfile();
        }

        public void ShowInfoInProfile()
        {
            foreach (Employee employ in Warehouse.Employees)
            {
                if (employ.Username == Username)
                {
                    NameLabel.Text = employ.Name;
                    RoleText.Text = employ.Role;
                    EmployeeIDText.Text = employ.EmployeeId;
                    break;
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {           
            DataWrapper dataWrapper = new DataWrapper
            {
                Warehouse = Warehouse,
                Suppliers = Supplier,
                PurchaseOrders = PurchaseOrder,
                ReturnOrders = ReturnOrder,
                Customers = Customer,
                OrderManager = OrderManager,
                SalesInvoices = SalesInvoice,
                Report = Report
            };

            string filePath = "Inventory_Management.dat";

            string fileJson = JsonSerializer.Serialize(dataWrapper, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, fileJson);

            Environment.Exit(0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Warehouse_Menu warehouse_Menu = new Warehouse_Menu(
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
            this.Close();
            warehouse_Menu.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Order_Menu order_Menu = new Order_Menu(
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
            this.Close();
            order_Menu.Show();
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Product_Menu product_Menu = new Product_Menu(
                Username, Warehouse, Supplier, PurchaseOrder, ReturnOrder, Customer, OrderManager, SalesInvoice, Report
                );
            this.Close();
            product_Menu.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Transaction_Menu transaction_Menu = new Transaction_Menu(
               Username, Warehouse, Supplier, PurchaseOrder, ReturnOrder, Customer, OrderManager, SalesInvoice, Report
               );
            this.Close();
            transaction_Menu.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            Report_Menu report_Menu = new Report_Menu(  Username, Warehouse, Supplier, PurchaseOrder, ReturnOrder, Customer, OrderManager, SalesInvoice, Report);
            this.Close();
            report_Menu.Show();
        }

        private void Profile_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataWrapper dataWrapper = new DataWrapper
            {
                Warehouse = Warehouse,
                Suppliers = Supplier,
                PurchaseOrders = PurchaseOrder,
                ReturnOrders = ReturnOrder,
                Customers = Customer,
                OrderManager = OrderManager,
                SalesInvoices = SalesInvoice,
                Report = Report
            };

            string filePath = "Inventory_Management.dat";

            string fileJson = JsonSerializer.Serialize(dataWrapper, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, fileJson);
        }
    }
}
