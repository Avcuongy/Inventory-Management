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
using System.IO;
using System.Text.Json;

namespace Inventory_Management
{
    public partial class Product_Update : Form
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

        public Product_Update(string username, Warehouse warehouse, List<Supplier> suppliers, List<PurchaseOrder> purchaseOrders,
         List<ReturnOrder> returnOrders, List<Customer> customers, OrderManager orderManager,
         List<SalesInvoice> salesInvoices, Report report)
        {
            InitializeComponent();
            Username = username;
            Warehouse = warehouse;
            Supplier = suppliers;
            PurchaseOrder = purchaseOrders;
            ReturnOrder = returnOrders;
            Customer = customers;
            OrderManager = orderManager;
            SalesInvoice = salesInvoices;
            Report = report;
            ShowComboboxAll();
            IdproductCombo.SelectedIndexChanged += IdproductCombo_SelectedIndexChanged;
        }
        public ProductChangeHandler ProductChangeHandlerUpdate;
        public void ShowComboboxAll()
        {
            List<Product> products = Warehouse.Products;
            List<string> productsId = new List<string>();
            HashSet<string> category = new HashSet<string>();
            List<string> suppliersName = new List<string>();

            foreach (Product product in products)
            {
                productsId.Add(product.ProductId);
            }
            IdproductCombo.DataSource = productsId;

            foreach (Product product in products)
            {
                category.Add(product.Category);
            }
            cBx_Category.DataSource = category.ToList();

            foreach (Supplier sup in Supplier)
            {
                suppliersName.Add(sup.Name);
            }
            comboBoxSupplier.DataSource = suppliersName;
        }
        public void ShowInfo()
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;

            string productId = IdproductCombo.Text.Trim();

            foreach (Product product in products)
            {
                if (productId == product.ProductId)
                {
                    tBx_Name_Product.Text = product.Name;
                    tBx_Quantity_Product.Text = product.Quantity.ToString();
                    tBx_Price_Product.Text = product.Price.ToString();
                    cBx_Category.Text = product.Category.ToString();

                    foreach (Supplier supplier in suppliers)
                    {
                        foreach (Product product1 in supplier.SuppliedProducts)
                        {
                            if (product1.ProductId == productId)
                            {
                                comboBoxSupplier.Text = supplier.Name;
                                return;
                            }
                        }
                    }
                }
            }
        }
        private void IdproductCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInfo();
        }
        private void buttonConfim_Click(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;

            string productId = IdproductCombo.Text.Trim();
            string nameProduct = tBx_Name_Product.Text.Trim();
            string category = cBx_Category.Text.Trim();

            int quantityProduct = 0;
            if (!int.TryParse(tBx_Quantity_Product.Text, out quantityProduct) || quantityProduct <= 0)
            {
                MessageBox.Show("Quantity Not Found");
                return;
            }

            double priceProduct = 0;
            if (!double.TryParse(tBx_Price_Product.Text, out priceProduct) || priceProduct <= 0)
            {
                MessageBox.Show("Base Price Not Found");
                return;
            }

            string supplierName = comboBoxSupplier.Text.Trim();

            string originalSupplier = "";

            // Cập nhật thông tin sản phẩm
            bool productUpdated = false;
            foreach (Product product in products)
            {
                if (product.ProductId == productId)
                {
                    product.Name = nameProduct;
                    product.Quantity = quantityProduct;
                    product.Category = category;
                    product.Price = priceProduct;
                    productUpdated = true;
                    break;
                }
            }

            if (!productUpdated)
            {
                MessageBox.Show("Product Not Found");
                return;
            }

            // Tìm nhà cung cấp ban đầu của sản phẩm
            foreach (Supplier supplier in suppliers)
            {
                foreach (Product product in supplier.SuppliedProducts)
                {
                    if (product.ProductId == productId)
                    {
                        originalSupplier = supplier.Name;
                        break;
                    }
                }
            }

            // Nếu nhà cung cấp ban đầu trùng với nhà cung cấp đã chọn
            if ((!string.IsNullOrEmpty(supplierName)))
            {
                if (supplierName != originalSupplier)
                {
                    bool supplierFound = false;
                    foreach (Supplier supplier in suppliers)
                    {
                        if (supplier.Name.Equals(supplierName))
                        {
                            supplierFound = true;
                            break;
                        }
                    }

                    if (supplierFound)
                    {
                        foreach (Supplier supplier in suppliers)
                        {
                            Product productToRemove = null;

                            foreach (Product suppliedProduct in supplier.SuppliedProducts)
                            {
                                if (suppliedProduct.ProductId == productId)
                                {
                                    productToRemove = suppliedProduct;
                                    break;
                                }
                            }

                            if (productToRemove != null)
                            {
                                supplier.SuppliedProducts.Remove(productToRemove);
                                break;
                            }
                        }
                        foreach (Supplier supplier in suppliers)
                        {
                            if (supplier.Name.Equals(supplierName))
                            {
                                foreach (Product product in products)
                                {
                                    if (product.ProductId == productId)
                                    {
                                        supplier.SuppliedProducts.Add(product);
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selected supplier not found.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Supplier Not Found");
                return;
            }

            MessageBox.Show("Successful");
            ProductChangeHandlerUpdate?.Invoke();
            this.Close();

        }

        private void Product_Update_FormClosed(object sender, FormClosedEventArgs e)
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
