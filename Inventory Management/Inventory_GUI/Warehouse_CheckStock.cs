using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management
{
    public partial class Warehouse_CheckStock : Form
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
        public Warehouse_CheckStock(string username,
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
            ShowCombobox();
        }

        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

        public void ShowCombobox()
        {
            List<Product> products = Warehouse.Products;
            List<string> productsID = new List<string>();
            foreach (Product product in products)
            {
                productsID.Add(product.ProductId);
            }
            comboBoxProductID.DataSource = productsID;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;
            List<Inventory> inventory = Warehouse.Inventory;

            Dictionary<string, int> productStock = inventory[0].ProductStock;

            string selectedProductID = comboBoxProductID.SelectedItem?.ToString();

            if (selectedProductID != null)
            {
                foreach (Product product in products)
                {
                    if (product.ProductId == selectedProductID)
                    {
                        textBox1.Text = product.Name;
                        textBox3.Text = product.Price.ToString();
                        textBox5.Text = product.Category;

                        foreach (string key in productStock.Keys)
                        {
                            if (product.ProductId == key)
                            {
                                textBox4.Text = productStock[key].ToString();
                                break;
                            }
                        }
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
        }
    }
}
