using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Inventory_Management
{
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
            _username = username;
            _warehouse = warehouse;
            _supplier = supplier;
            _purchaseOrder = purchaseOrder;
            _returnOrder = returnOrder;
            _customer = customer;
            _orderManager = orderManager;
            _salesInvoice = salesInvoice;
            _report = report;
            ShowDataGridView();
        }
        // Back to menu
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Profile profile = new Profile(
                                    _username,
                                    _warehouse,
                                    _supplier,
                                    _purchaseOrder,
                                    _returnOrder,
                                    _customer,
                                    _orderManager,
                                    _salesInvoice,
                                    _report
                );
            profile.Show();
            this.Hide();
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

            List<Product> products = _warehouse.Products;
            List<Inventory> inventory = _warehouse.Inventory;
            List<Supplier> suppliers = _supplier;

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
                                    _username,
                                    _warehouse,
                                    _supplier,
                                    _purchaseOrder,
                                    _returnOrder,
                                    _customer,
                                    _orderManager,
                                    _salesInvoice,
                                    _report
                );
            warehouse_CheckStock.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Warehouse_Export warehouse_Export = new Warehouse_Export(
                _username,
                _warehouse,
                _supplier,
                _purchaseOrder,
                _returnOrder,
                _customer,
                _orderManager,
                _salesInvoice,
                _report
            );
            warehouse_Export.StockLevelChanged += Warehouse_Export_StockLevelChanged;
            warehouse_Export.Show();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            string productId = textBox1.Text.Trim();

            ShowWarehouse.CurrentCell = null;

            DataTable dt = new DataTable();

            dt.Columns.Add("Product ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Supplier");
            dt.Columns.Add("Current Stock Level");

            List<Product> products = _warehouse.Products;
            List<Inventory> inventory = _warehouse.Inventory;
            List<Supplier> suppliers = _supplier;

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
                    if (product.ProductId.Equals(productId, StringComparison.OrdinalIgnoreCase) &&
                        productStock.TryGetValue(product.ProductId, out int currentStockLevel))
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
                _username,
                _warehouse,
                _supplier,
                _purchaseOrder,
                _returnOrder,
                _customer,
                _orderManager,
                _salesInvoice,
                _report
            );
            warehouse_Import.StockLevelChanged += Warehouse_Import_StockLevelChanged;
            warehouse_Import.Show();
        }

        private void Warehouse_Export_StockLevelChanged()
        {
            ShowDataGridView();
        }
        private void Warehouse_Import_StockLevelChanged()
        {
            ShowDataGridView();
        }
    }
}
