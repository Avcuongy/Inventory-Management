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
            _username = username;
            _warehouse = warehouse;
            _supplier = supplier;
            _purchaseOrder = purchaseOrder;
            _returnOrder = returnOrder;
            _customer = customer;
            _orderManager = orderManager;
            _salesInvoice = salesInvoice;
            _report = report;
            ShowCombobox();
        }
        public void ShowCombobox()
        {
            List<Product> products = _warehouse.Products;
            List<string> productsID = new List<string>();
            foreach (Product product in products)
            {
                productsID.Add(product.ProductId);
            }
            comboBoxProductID.DataSource = productsID;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> products = _warehouse.Products;
            List<Inventory> inventory = _warehouse.Inventory;

            Dictionary<string, int> productStock = inventory[0].ProductStock;

            string selectedProductID = comboBoxProductID.SelectedItem.ToString();

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
    }
}
