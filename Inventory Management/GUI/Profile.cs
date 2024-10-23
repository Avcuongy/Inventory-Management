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
            _username = username;
            _warehouse = warehouse;
            _supplier = supplier;
            _purchaseOrder = purchaseOrder;
            _returnOrder = returnOrder;
            _customer = customer;
            _orderManager = orderManager;
            _salesInvoice = salesInvoice;
            _report = report;
            ShowInfoInProfile();

        }
        public void ShowInfoInProfile()
        {
            foreach (Employee employ in _warehouse.Employees)
            {
                if (employ.Username == _username)
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
            string filePath = "Inventory_Management.dat";

            Dictionary<string, object> allData = new Dictionary<string, object>
            { 
                { "Warehouse", _warehouse },
                { "Supplier", _supplier },
                { "PurchaseOrder", _purchaseOrder },
                { "ReturnOrder", _returnOrder },
                { "Customer", _customer },
                { "OrderManager", _orderManager },
                { "SalesInvoice", _salesInvoice },
                { "Report", _report }
            };

            string fileContent = JsonSerializer.Serialize(allData);

            File.WriteAllText(filePath, fileContent);
        }
    }
}
