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
        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

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
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            textBox3.TextChanged += textBox1_TextChanged;

        }
        public event StockLevelChangedHandler StockLevelChanged;

        public void ShowCombobox()
        {
            List<Product> products = Warehouse.Products;
            List<string> productsId = new List<string>();
            foreach (Product product in products)
            {
                productsId.Add(product.ProductId);
            }
            comboBox1.DataSource = productsId;
        }
        public void ShowInfo()
        {
            List<Product> products = Warehouse.Products;
            List<Inventory> inventory = Warehouse.Inventory;
            List<Supplier> suppliers = Supplier;

            string selectedProductId = comboBox1.SelectedItem?.ToString();

            if (selectedProductId != null)
            {
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
            else
            {
                textBox2.Text = "N/A";
                textBox6.Text = "N/A";
                textBox5.Text = "N/A";
                textBox3.Text = "N/A";
                textBox1.Text = "N/A";
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;

            int quantity_Import = 0;

            if (int.TryParse(textBox3.Text.Trim(), out int quantity))
            {
                quantity_Import = quantity;
            }

            string selectedProductId = comboBox1.SelectedItem?.ToString();

            foreach (Product product in products)
            {
                if (Warehouse.CheckProductId(selectedProductId))
                {
                    if (quantity_Import > 0)
                    {
                        textBox1.Text = (quantity_Import * product.Price).ToString();
                    }
                    else
                    {
                        textBox1.Text = "0";
                    }
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;
            List<Inventory> inventory = Warehouse.Inventory;
            List<ReturnOrder> returnOrder = ReturnOrder;

            Dictionary<string, int> productStock = inventory[0].ProductStock;

            string selectedProductId = comboBox1.SelectedItem?.ToString();

            DialogResult dialogResult = MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo);

            int quantity_Import = 0;

            if (int.TryParse(textBox3.Text.Trim(), out int quantity))
            {
                quantity_Import = quantity;
            }

            if (dialogResult == DialogResult.Yes && quantity_Import > 0 && selectedProductId != null)
            {
                inventory[0].AddStock(selectedProductId, quantity_Import);

                StockLevelChanged?.Invoke();

                MessageBox.Show("successful");

                this.Close();
            }
            else
            {
                MessageBox.Show("There may not be enough stock available");
            }
        }

        private void Warehouse_Import_FormClosed(object sender, FormClosedEventArgs e)
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
