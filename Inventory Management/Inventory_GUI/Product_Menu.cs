using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Inventory_Management
{
    public delegate void ProductChangeHandler();
    public partial class Product_Menu : Form
    {
        private List<Product> products;
        private string _username;
        private Warehouse _warehouse;
        private List<Supplier> _supplier = new List<Supplier>();
        private List<PurchaseOrder> _purchaseOrder = new List<PurchaseOrder>();
        private List<ReturnOrder> _returnOrder = new List<ReturnOrder>();
        private List<Customer> _customer = new List<Customer>();
        private OrderManager _orderManager;
        private List<SalesInvoice> _salesInvoice = new List<SalesInvoice>();
        private Report _report;
        public List<Product> Products { get => products; set => products = value; }
        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

        public Product_Menu(string username,
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
            ShowData();
        }
        private void Return_Profile(object sender, EventArgs e)
        {
            Profile profileForm = new Profile(Username,
                                              Warehouse,
                                              Supplier,
                                              PurchaseOrder,
                                              ReturnOrder,
                                              Customer,
                                              OrderManager,
                                              SalesInvoice,
                                              Report);
            this.Close();
            profileForm.Show();
        }
        public void ShowData()
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;

            DataTable dt = new DataTable();
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Base Price");
            dt.Columns.Add("Supplier");

            foreach (Product product in products)
            {
                DataRow dataRow = dt.NewRow();
                dataRow["Product ID"] = product.ProductId;
                dataRow["Name"] = product.Name;
                dataRow["Category"] = product.Category;
                dataRow["Quantity"] = product.Quantity;
                dataRow["Base Price"] = product.Price.ToString();
                foreach (Supplier supplier in suppliers)
                {
                    string supplierName = supplier.GetSupplierName(product.ProductId);
                    if (supplierName != "N/A")
                    {
                        dataRow["Supplier"] = supplierName;
                        break;
                    }
                }
                dt.Rows.Add(dataRow);
            }

            dataGridView1.DataSource = dt;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;

            string productId = textBox1.Text.Trim();

            DataTable dt = new DataTable();

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Base Price");
            dt.Columns.Add("Supplier");

            if (string.IsNullOrEmpty(productId))
            {
                foreach (Product product in products)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow["Product ID"] = product.ProductId;
                    dataRow["Name"] = product.Name;
                    dataRow["Category"] = product.Category;
                    dataRow["Quantity"] = product.Quantity;
                    dataRow["Base Price"] = product.Price.ToString();
                    foreach (Supplier supplier in suppliers)
                    {
                        string supplierName = supplier.GetSupplierName(product.ProductId);
                        if (supplierName != "N/A")
                        {
                            dataRow["Supplier"] = supplierName;
                            break;
                        }
                    }
                    dt.Rows.Add(dataRow);
                }
            }
            else
            {
                Product selectedProduct = null;

                foreach (Product product in products)
                {
                    if (product.ProductId.ToLower() == productId.ToLower())
                    {
                        selectedProduct = product;
                        break;
                    }
                }

                if (selectedProduct != null)
                {
                    DataRow dataRow = dt.NewRow();
                    dataRow["Product ID"] = selectedProduct.ProductId;
                    dataRow["Name"] = selectedProduct.Name;
                    dataRow["Category"] = selectedProduct.Category;
                    dataRow["Quantity"] = selectedProduct.Quantity;
                    dataRow["Base Price"] = selectedProduct.Price.ToString();

                    foreach (Supplier supplier in suppliers)
                    {
                        string supplierName = supplier.GetSupplierName(selectedProduct.ProductId);
                        if (supplierName != "N/A")
                        {
                            dataRow["Supplier"] = supplierName;
                            break;
                        }
                    }
                    dt.Rows.Add(dataRow);
                }
            }

            dataGridView1.DataSource = dt;
        }

        private void button_Add_Product_Click(object sender, EventArgs e)
        {
            Product_Add product_Add = new Product_Add(
                Username,
                Warehouse,
                Supplier,
                PurchaseOrder,
                ReturnOrder,
                Customer,
                OrderManager,
                SalesInvoice,
                Report);
            product_Add.ProductChangeHandler += ShowData;
            product_Add.ShowDialog();
        }

        private void button_Update_Product_Click(object sender, EventArgs e)
        {
            Product_Update product_Update = new Product_Update(
                Username,
                Warehouse,
                Supplier,
                PurchaseOrder,
                ReturnOrder,
                Customer,
                OrderManager,
                SalesInvoice,
                Report);
            product_Update.ProductChangeHandlerUpdate += ShowData;
            product_Update.ShowDialog();
        }
    }
}
