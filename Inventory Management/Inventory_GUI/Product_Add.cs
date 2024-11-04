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
    public partial class Product_Add : Form
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

        private DataGridView dataGridView1;
        public Product_Add(string username, Warehouse warehouse, List<Supplier> supplier, List<PurchaseOrder> purchaseOrder, List<ReturnOrder> returnOrder, List<Customer> customer, OrderManager orderManager, List<SalesInvoice> salesInvoice, Report report)
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
            ShowInfo();
        }
        public ProductChangeHandler ProductChangeHandler;
        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }
        public void ShowInfo()
        {
            List<Product> products = Warehouse.Products;
            HashSet<string> productCategory = new HashSet<string>();
            List<Supplier> suppliers = Supplier;
            List<string> supplierName = new List<string>();

            foreach (Supplier supplier in suppliers)
            {
                supplierName.Add(supplier.Name);
            }
            comboBox1.DataSource = supplierName;

            tBx_ID_Product.Text = $"P{products.Count + 1}";

            foreach (Product product in products)
            {
                if (product is Phone || product is Tablet || product is Keyboard || product is Mouse || product is Headphone)
                {
                    string category = product.Category;
                    productCategory.Add(category);
                }
            }

            cBx_Category.DataSource = productCategory.ToList();
        }
        private void Confirm_Click_Click(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;
            List<Inventory> inventory = Warehouse.Inventory;

            Dictionary<string, int> stock = inventory[0].ProductStock;

            string productId = tBx_ID_Product.Text.Trim();
            string nameProduct = tBx_Name_Product.Text.Trim();

            int quantity_add = 1;
            if (!int.TryParse(tBx_Quantity_Product.Text, out quantity_add) || quantity_add <= 0)
            {
                MessageBox.Show("Invalid quantity.");
                return;
            }

            int product_price = 0;
            if (!int.TryParse(tBx_Price_Product.Text, out product_price) || product_price <= 0)
            {
                MessageBox.Show("Invalid price.");
                return;
            }

            string product_Category = cBx_Category.SelectedItem != null ? cBx_Category.SelectedItem.ToString() : "";
            string supplierName = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "N/A";

            if (string.IsNullOrEmpty(productId) || string.IsNullOrEmpty(nameProduct) || string.IsNullOrEmpty(product_Category))
            {
                MessageBox.Show("Error, Try Again");
                return;
            }

            foreach (Product product in products)
            {
                if (productId == product.ProductId)
                {
                    MessageBox.Show("Product ID already exists.");
                    return;
                }
            }

            Product product_has_add = null;
            if (product_Category == "Phone")
            {
                product_has_add = new Phone(productId, nameProduct, "Phone", quantity_add, product_price);
            }
            else if (product_Category == "Tablet")
            {
                product_has_add = new Tablet(productId, nameProduct, "Tablet", quantity_add, product_price);
            }
            else if (product_Category == "Headphone")
            {
                product_has_add = new Headphone(productId, nameProduct, "Headphone", quantity_add, product_price);
            }
            else if (product_Category == "Keyboard")
            {
                product_has_add = new Keyboard(productId, nameProduct, "Keyboard", quantity_add, product_price);
            }
            else if (product_Category == "Mouse")
            {
                product_has_add = new Mouse(productId, nameProduct, "Mouse", quantity_add, product_price);
            }
            else
            {
                MessageBox.Show("Invalid product category.");
                return;
            }

            products.Add(product_has_add);
            stock.Add(productId, 0);

            foreach (Supplier supplier in suppliers)
            {
                if (supplierName.ToLower() == supplier.Name.ToLower())
                {
                    supplier.SuppliedProducts.Add(product_has_add);
                    break;
                }
            }

            MessageBox.Show("Successful");
            if (ProductChangeHandler != null)
            {
                ProductChangeHandler.Invoke();
            }
            this.Close();

        }

        private void Product_Add_FormClosed(object sender, FormClosedEventArgs e)
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
