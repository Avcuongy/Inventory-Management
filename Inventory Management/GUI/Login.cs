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
            _warehouse = warehouse;
            _supplier = supplier;
            _purchaseOrder = purchaseOrder;
            _returnOrder = returnOrder;
            _customer = customer;
            _orderManager = orderManager;
            _salesInvoice = salesInvoice;
            _report = report;
        }
        private void Login_Button_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;
            if (_warehouse.CheckUser(username, password))
            {
                MessageBox.Show("Login Successful!");
                this.Hide();
                Profile profile = new Profile(
                                    username, 
                                    _warehouse,
                                    _supplier,
                                    _purchaseOrder,
                                    _returnOrder,
                                    _customer,
                                    _orderManager,
                                    _salesInvoice,
                                    _report);
                profile.Show();
            }    
        }
    }
}
