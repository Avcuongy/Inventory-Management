using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Inventory_Management
{
    public delegate void StockLevelChangedHandler();
    public partial class Warehouse_Menu : Form
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

        public Warehouse_Menu(string username,
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
            ShowDataGridView();
        }
        // Back to menu
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile(
                                    Username,
                                    Warehouse,
                                    Supplier,
                                    PurchaseOrder,
                                    ReturnOrder,
                                    Customer,
                                    OrderManager,
                                    SalesInvoice,
                                    Report
                );
            profile.Show();
            this.Close();
        }

        public void ShowDataGridView()
        {
            ShowWarehouse.CurrentCell = null;

            DataTable dt = new DataTable();

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Current Stock Level");

            List<Product> products = Warehouse.Products;
            List<Inventory> inventory = Warehouse.Inventory;
            List<Supplier> suppliers = Supplier;

            Dictionary<string, int> productStock = inventory[0].ProductStock;

            foreach (Product product in products)
            {
                if (productStock.TryGetValue(product.ProductId, out int currentStockLevel))
                {
                    Supplier foundSupplier = null;
                    foreach (Supplier sup in suppliers)
                    {
                        foreach (Product suppliedProduct in sup.SuppliedProducts)
                        {
                            if (suppliedProduct.ProductId == product.ProductId)
                            {
                                foundSupplier = sup;
                                break;
                            }
                        }
                        if (foundSupplier != null)
                        {
                            break;
                        }
                    }
                    if (foundSupplier != null)
                    {
                        dt.Rows.Add(product.ProductId, product.Name, product.Category, foundSupplier.Name, currentStockLevel);
                    }
                    else
                    {
                        dt.Rows.Add(product.ProductId, product.Name, product.Category, "N/A", currentStockLevel);
                    }
                }
            }

            ShowWarehouse.DataSource = dt;
        }

        private void Check_Stock_Click(object sender, EventArgs e)
        {
            Warehouse_CheckStock warehouse_CheckStock = new Warehouse_CheckStock(
                                    Username,
                                    Warehouse,
                                    Supplier,
                                    PurchaseOrder,
                                    ReturnOrder,
                                    Customer,
                                    OrderManager,
                                    SalesInvoice,
                                    Report
                );
            warehouse_CheckStock.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Warehouse_Export warehouse_Export = new Warehouse_Export(
                Username,
                Warehouse,
                Supplier,
                PurchaseOrder,
                ReturnOrder,
                Customer,
                OrderManager,
                SalesInvoice,
                Report
            );
            warehouse_Export.StockLevelChanged += Warehouse_Export_StockLevelChanged;
            warehouse_Export.ShowDialog();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            string productId = textBox1.Text.Trim().ToLower();

            ShowWarehouse.CurrentCell = null;

            DataTable dt = new DataTable();

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Current Stock Level");

            List<Product> products = Warehouse.Products;
            List<Inventory> inventory = Warehouse.Inventory;
            List<Supplier> suppliers = Supplier;

            Dictionary<string, int> productStock = inventory[0].ProductStock;

            if (string.IsNullOrEmpty(productId))
            {
                foreach (Product product in products)
                {
                    if (productStock.TryGetValue(product.ProductId, out int currentStockLevel))
                    {
                        Supplier foundSupplier = null;

                        foreach (Supplier sup in suppliers)
                        {
                            foreach (Product suppliedProduct in sup.SuppliedProducts)
                            {
                                if (suppliedProduct.ProductId == product.ProductId)
                                {
                                    foundSupplier = sup;
                                    break;
                                }
                            }
                            if (foundSupplier != null)
                            {
                                break;
                            }
                        }

                        if (foundSupplier != null)
                        {
                            dt.Rows.Add(product.ProductId, product.Name, product.Category, foundSupplier.Name, currentStockLevel);
                        }
                        else
                        {
                            dt.Rows.Add(product.ProductId, product.Name, product.Category, "N/A", currentStockLevel);
                        }
                    }
                }
            }
            else
            {
                foreach (Product product in products)
                {
                    if (product.ProductId.ToLower() == productId && productStock.TryGetValue(product.ProductId, out int currentStockLevel))
                    {
                        Supplier foundSupplier = null;

                        foreach (Supplier sup in suppliers)
                        {
                            foreach (Product suppliedProduct in sup.SuppliedProducts)
                            {
                                if (suppliedProduct.ProductId == product.ProductId)
                                {
                                    foundSupplier = sup;
                                    break;
                                }
                            }
                            if (foundSupplier != null)
                            {
                                break;
                            }
                        }

                        if (foundSupplier != null)
                        {
                            dt.Rows.Add(product.ProductId, product.Name, product.Category, foundSupplier.Name, currentStockLevel);
                        }
                        else
                        {
                            dt.Rows.Add(product.ProductId, product.Name, product.Category, "N/A", currentStockLevel);
                        }

                        break;
                    }
                }
            }

            ShowWarehouse.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Warehouse_Import warehouse_Import = new Warehouse_Import(
                Username,
                Warehouse,
                Supplier,
                PurchaseOrder,
                ReturnOrder,
                Customer,
                OrderManager,
                SalesInvoice,
                Report
            );
            warehouse_Import.StockLevelChanged += Warehouse_Import_StockLevelChanged;
            warehouse_Import.ShowDialog();
        }

        private void Warehouse_Export_StockLevelChanged()
        {
            ShowDataGridView();
        }
        private void Warehouse_Import_StockLevelChanged()
        {
            ShowDataGridView();
        }

        private void Warehouse_Menu_FormClosed(object sender, FormClosedEventArgs e)
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
