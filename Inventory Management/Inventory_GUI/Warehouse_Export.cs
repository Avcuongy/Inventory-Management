using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Inventory_Management
{
    public partial class Warehouse_Export : Form
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

        public Warehouse_Export(string username,
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
            ShowComboBox();
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            textBox7.TextChanged += textBox1_TextChanged;

        }

        public event StockLevelChangedHandler StockLevelChanged;

        public void ShowComboBox()
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

            string selectedProductId = comboBox1.SelectedItem.ToString().Trim();

            foreach (Product product in products)
            {
                if (product.ProductId == selectedProductId)
                {
                    textBox2.Text = product.Name;
                    textBox6.Text = product.Category;
                    textBox7.Text = "1";
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

            textBox8.Text = DateTime.Now.ToString("dd/MM/yyyy");
            textBox7.Text = "1";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInfo();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;
            List<Inventory> inventory = Warehouse.Inventory;
            List<Supplier> suppliers = Supplier;
            List<ReturnOrder> returnOrders = ReturnOrder;

            string selectedProductId = comboBox1.SelectedItem.ToString();

            Dictionary<string, int> productStock = inventory[0].ProductStock;

            int quantity_Selected_Product = 0;

            foreach (String key in productStock.Keys)
            {
                if (key == selectedProductId)
                {
                    quantity_Selected_Product = productStock[key];
                }
            }

            string reason = textBox3.Text.Trim();

            DateTime returnDate = DateTime.Now;

            if (DateTime.TryParse(textBox8.Text.Trim(), out DateTime dateNow))
            {
                returnDate = dateNow;
            }

            int quantity_Export = 0;

            if (int.TryParse(textBox7.Text.Trim(), out int quantity))
            {
                quantity_Export = quantity;
            }

            DialogResult dialogResult = MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes && quantity_Selected_Product > 0 && quantity_Export > 0 && quantity_Selected_Product >= quantity_Export)
            {

                Product product_In_ReturnOrder = null;

                foreach (Product product in products)
                {
                    if (selectedProductId == product.ProductId)
                    {
                        switch (product.Category)
                        {
                            case "Phone":
                                {
                                    product_In_ReturnOrder = new Phone(product.ProductId, product.Name, product.Category, quantity_Export, product.Price*quantity_Export);
                                    break;
                                }
                            case "Tablet":
                                {
                                    product_In_ReturnOrder = new Tablet(product.ProductId, product.Name, product.Category, quantity_Export, product.Price * quantity_Export);
                                    break;
                                }
                            case "Headphone":
                                {
                                    product_In_ReturnOrder = new Headphone(product.ProductId, product.Name, product.Category, quantity_Export, product.Price * quantity_Export);
                                    break;
                                }
                            case "Mouse":
                                {
                                    product_In_ReturnOrder = new Mouse(product.ProductId, product.Name, product.Category, quantity_Export, product.Price * quantity_Export);
                                    break;
                                }
                            case "Keyboard":
                                {
                                    product_In_ReturnOrder = new Keyboard(product.ProductId, product.Name, product.Category, quantity_Export, product.Price * quantity_Export);
                                    break;
                                }
                        }
                        break;
                    }
                }

                returnOrders.Add(
                     new ReturnOrder(
                     $"R{returnOrders.Count + 1}",
                     product_In_ReturnOrder,
                     reason,
                     returnDate,
                     "Pending"));

                inventory[0].RemoveStock(selectedProductId, quantity_Export);

                StockLevelChanged?.Invoke();

                MessageBox.Show("successful");

                this.Hide();
            }
            else if (quantity_Selected_Product <= 0 || quantity_Selected_Product < quantity_Export || quantity_Export <= 0)
            {
                MessageBox.Show("There may not be enough stock available");
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;

            string selectedProductId = comboBox1.SelectedItem.ToString();

            int quantity_Export = 0;

            if (int.TryParse(textBox7.Text, out int quanity))
            {
                quantity_Export = quanity;
            }

            foreach (Product product in products)
            {
                if (product.ProductId == selectedProductId)
                {
                    if (quantity_Export > 0)
                    {
                        textBox1.Text = (quantity_Export * product.Price).ToString();
                    }
                    else
                    {
                        textBox1.Text = "0";
                    }
                    break;
                }
            }        
        }
    }
}