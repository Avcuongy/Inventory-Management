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
    public partial class Warehouse_Import : Form
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

        public Warehouse_Import(string username,
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
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            textBox3.TextChanged += textBox1_TextChanged;

        }
        public event StockLevelChangedHandler StockLevelChanged;

        public void ShowCombobox()
        {
            List<Product> products = _warehouse.Products;
            List<string> productsId = new List<string>();
            foreach (Product product in products)
            {
                productsId.Add(product.ProductId);
            }
            comboBox1.DataSource = productsId;
        }
        public void ShowInfo()
        {
            List<Product> products = _warehouse.Products;
            List<Inventory> inventory = _warehouse.Inventory;
            List<Supplier> suppliers = _supplier;

            string selectedProductId = comboBox1.SelectedItem.ToString();

            foreach (Product product in products)
            {
                if (product.ProductId == selectedProductId)
                {
                    textBox2.Text = product.Name;
                    textBox6.Text = product.Category;
                    textBox5.Text = product.Price.ToString();
                    textBox3.Text = "1";
                    textBox1.Text = product.Price.ToString();

                    foreach (Supplier sup in suppliers)
                    {
                        foreach (Product suppliedProduct in sup.SuppliedProducts)
                        {
                            if (suppliedProduct.ProductId == selectedProductId)
                            {
                                textBox4.Text = sup.Name;
                                break;
                            }
                        }
                    }
                    break;
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Product> products = _warehouse.Products;

            int quantity_Import = 0;

            if (int.TryParse(textBox3.Text.Trim(), out int quantity))
            {
                quantity_Import = quantity;
            }    
            
            string selectedProductId = comboBox1.SelectedItem.ToString();

            foreach (Product product in products)
            {
                if (_warehouse.CheckProductId(selectedProductId))
                {
                    textBox1.Text = (quantity_Import* product.Price).ToString();
                    break;
                }    
            }    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> products = _warehouse.Products;
            List<Inventory> inventory = _warehouse.Inventory;

            Dictionary<string, int> productStock = inventory[0].ProductStock;

            string selectedProductId = comboBox1.SelectedItem.ToString();

            DialogResult dialogResult = MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo);

            int quantity_Import = 0;

            if (int.TryParse(textBox3.Text.Trim(), out int quantity))
            {
                quantity_Import = quantity;
            }

            if (dialogResult == DialogResult.Yes && quantity_Import > 0)
            {
                inventory[0].AddStock(selectedProductId, quantity_Import);
                StockLevelChanged?.Invoke();
                this.Hide();
            }
            else
            {
                MessageBox.Show("There may not be enough stock available");
            }
        }
    }
}
