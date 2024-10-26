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
            ShowComboboxProductId();
            IdproductCombo.SelectedIndexChanged += IdproductCombo_SelectedIndexChanged;
        }
        public ProductChangeHandler ProductChangeHandlerUpdate;
        public void ShowComboboxProductId()
        {
            List<Product> products = Warehouse.Products;
            List<string> productsId = new List<string>();
            foreach (Product product in products)
            {
                productsId.Add(product.ProductId);
            }
            IdproductCombo.DataSource = productsId;
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
                                tBx_Supplier_Product.Text = supplier.Name;
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

        private void button1_Click(object sender, EventArgs e)
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;

            string productId = IdproductCombo.Text.Trim();
            string nameProduct = tBx_Name_Product.Text.Trim();
            string category = cBx_Category.Text.Trim();
            int quantityProduct = 0;

            if (int.TryParse(tBx_Quantity_Product.Text, out int quantity))
            {
                quantityProduct = quantity;
            }

            int priceProduct = 0;

            if (int.TryParse(tBx_Price_Product.Text, out int price))
            {
                priceProduct = price;
            }

            string supplierName = IdproductCombo.Text.Trim();
            string originalSupplier = "";

            foreach (Product product in products)
            {
                if (productId == product.ProductId)
                {
                    product.Name = nameProduct;
                    product.Quantity = quantityProduct;
                    product.Category = category;
                    product.Price = priceProduct;
                    break;
                }
            }

            foreach (Supplier supplier in suppliers)
            {
                foreach (Product pro in supplier.SuppliedProducts)
                {
                    if (productId == pro.ProductId)
                    {
                        originalSupplier = supplier.Name;
                        break;
                    }
                }
                if (!originalSupplier.Equals("")) break;
            }

            if (originalSupplier.Equals(supplierName))
            {
                foreach (Supplier supplier in suppliers)
                {
                    foreach (Product product in products)
                    {
                        if (productId == product.ProductId)
                        {
                            product.Name = nameProduct;
                            product.Quantity = quantityProduct;
                            product.Category = category;
                            product.Price = priceProduct;
                            break;
                        }
                    }
                }
            }
            else
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
                        foreach (Product pro in supplier.SuppliedProducts)
                        {
                            if (productId == pro.ProductId)
                            {
                                supplier.SuppliedProducts.Remove(pro);
                                break;
                            }
                        }
                    }

                    foreach (Supplier supplier in suppliers)
                    {
                        if (supplier.Name.Equals(supplierName))
                        {
                            Product productToAdd = null;

                            foreach (Product product in products)
                            {
                                if (productId == product.ProductId)
                                {
                                    productToAdd = product;
                                    break;
                                }
                            }

                            if (productToAdd != null)
                            {
                                supplier.SuppliedProducts.Add(productToAdd);
                            }
                            break;
                        }
                    }
                }
            }

        }
    }
}
