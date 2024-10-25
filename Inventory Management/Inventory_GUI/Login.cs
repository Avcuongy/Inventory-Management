using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management
{
    public partial class Login : Form
    {
        private Warehouse _warehouse;
        private List<Supplier> _supplier = new List<Supplier>();
        private List<PurchaseOrder> _purchaseOrder = new List<PurchaseOrder>();
        private List<ReturnOrder> _returnOrder = new List<ReturnOrder>();
        private List<Customer> _customer = new List<Customer>();
        private OrderManager _orderManager;
        private List<SalesInvoice> _salesInvoice = new List<SalesInvoice>();
        private Report _report;
        public Login(Warehouse warehouse,
            List<Supplier> supplier,
            List<PurchaseOrder> purchaseOrder,
            List<ReturnOrder> returnOrder,
            List<Customer> customer,
            OrderManager orderManager,
            List<SalesInvoice> salesInvoice,
            Report report)
        {
            InitializeComponent();
            Warehouse = warehouse;
            Supplier = supplier;
            PurchaseOrder = purchaseOrder;
            ReturnOrder = returnOrder;
            Customer = customer;
            OrderManager = orderManager;
            SalesInvoice = salesInvoice;
            Report = report;
        }

        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

        private void Login_Button_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;
            if (Warehouse.CheckUser(username, password))
            {
                MessageBox.Show("Login Successful!");
                this.Hide();
                Profile profile = new Profile(
                                    username, 
                                    Warehouse,
                                    Supplier,
                                    PurchaseOrder,
                                    ReturnOrder,
                                    Customer,
                                    OrderManager,
                                    SalesInvoice,
                                    Report);
                profile.Show();
            }    
        }
    }
}
