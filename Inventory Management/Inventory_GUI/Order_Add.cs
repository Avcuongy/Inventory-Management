using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Inventory_Management
{
    public partial class Order_Add : Form
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

        public Order_Add(string username,
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
            ShowComboBoxOrder();
            ShowInfoOrder();
            comboBoxProduct.SelectedIndexChanged += comboBoxProduct_SelectedIndexChanged;
            QuantityTextOrder.TextChanged += textBox3_TextChanged;
        }
        public OrderChangeInformation OrderChangeAdd;
        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

        public void ShowComboBoxOrder()
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;
            List<string> productsId = new List<string>();
            List<string> suppliersName = new List<string>();

            foreach (Supplier supplier in suppliers)
            {
                suppliersName.Add(supplier.Name);
            }
            comboBoxSupplier.DataSource = suppliersName;
        }
        public void ShowInfoOrder()
        {
            OrderIDText.Text = GetOrderId();
            productsOrder.Text = "0";
        }
        private string GetOrderId()
        {
            return $"PD{PurchaseOrder.Count + 1}";
        }
        private Product GetProductById(string productId)
        {
            return Warehouse.Products.Find(product => product.ProductId == productId);
        }
        private Supplier GetSupplierForProduct(string productId)
        {
            foreach (Supplier supplier in Supplier)
            {
                if (supplier.SuppliedProducts.Exists(product => product.ProductId == productId))
                {
                    return supplier;
                }
            }
            return null;
        }
        private int GetQuantity()
        {
            return int.TryParse(QuantityTextOrder.Text, out int quantity) ? quantity : 0;
        }
        private double GetTotalPrice()
        {
            return double.TryParse(TotalText.Text, out double totalPrice) ? totalPrice : 0;
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string selectedProductId = comboBoxProduct.SelectedItem?.ToString();
            int quantity_add_order = GetQuantity();

            Product product = GetProductById(selectedProductId);
            if (product != null)
            {
                TotalText.Text = (quantity_add_order > 0 ? product.Price * quantity_add_order : 0).ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string supplierName = comboBoxSupplier.SelectedItem != null ? comboBoxSupplier.SelectedItem.ToString() : "";

            if (!string.IsNullOrEmpty(supplierName))
            {
                bool supplierFound = false;

                foreach (Supplier supplier in Supplier)
                {
                    if (supplier.Name == supplierName)
                    {
                        panel2.Visible = true;
                        labelStatus.Visible = true;
                        statusText.Visible = true;
                        supplierFound = true;
                        button1.Visible = false;
                        AddButton.Visible = true;
                        comboBoxSupplier.Enabled = false;
                        productsOrder.Visible = true;
                        label12.Visible = true;
                        ShowInfoProductOfSupplier();
                        break;
                    }
                }

                if (!supplierFound)
                {
                    MessageBox.Show("Supplier Not Found");
                }
            }
            else
            {
                MessageBox.Show("Supplier Not Found");
            }
        }
        public void ShowInfoProductOfSupplier()
        {
            List<Product> products = Warehouse.Products;
            List<Supplier> suppliers = Supplier;
            List<string> productsOfSupplier = new List<string>();

            string supplierName = comboBoxSupplier.SelectedItem.ToString();

            Supplier selectedSupplier = null;

            foreach (Supplier supplier in suppliers)
            {
                if (supplier.Name == supplierName)
                {
                    selectedSupplier = supplier;
                    break;
                }
            }

            if (selectedSupplier != null)
            {
                foreach (Product product in selectedSupplier.SuppliedProducts)
                {
                    productsOfSupplier.Add(product.ProductId);
                }
            }
            else
            {
                MessageBox.Show("Not Found Products Of Supplier");
            }

            comboBoxProduct.DataSource = productsOfSupplier;

            QuantityTextOrder.Text = "0";

            TotalText.Text = "0";
        }
        private void comboBoxProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string productId = comboBoxProduct.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(productId))
            {
                foreach (Product product in Warehouse.Products)
                {
                    if (product.ProductId == productId)
                    {
                        nameProduct.Text = product.Name;
                        QuantityTextOrder.Text = product.Quantity.ToString();
                        TotalText.Text = product.Price.ToString();
                        break;
                    }
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<Product> products = new List<Product>();
            List<Supplier> suppliers = Supplier;

            string orderId = OrderIDText.Text.Trim();
            string productId = comboBoxProduct.SelectedItem?.ToString();
            string quantity = QuantityTextOrder.Text.Trim();
            string total = TotalText.Text.Trim();
            string status = "Pending";
            string supplierName = comboBoxSupplier.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(orderId) || string.IsNullOrEmpty(productId) ||
                string.IsNullOrEmpty(quantity) || string.IsNullOrEmpty(total) ||
                string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Not Found Information");
                return;
            }

            if (!int.TryParse(quantity, out int quantityInt) || quantityInt <= 0)
            {
                MessageBox.Show("Quantity Not Found");
                return;
            }

            Product selectedProduct = GetProductById(productId); 
            if (selectedProduct == null)
            {
                MessageBox.Show("Not Found Product");
                return;
            }

            selectedProduct.Quantity = int.Parse(quantity);
            selectedProduct.Price = double.Parse(total);

            Supplier supplier = null;
            foreach (Supplier sup in suppliers)
            {
                if (sup.Name == supplierName)
                {
                    supplier = sup;
                    break;
                }
            }

            PurchaseOrder existingOrder = null;
            foreach (PurchaseOrder order in PurchaseOrder)
            {
                if (order.OrderId == orderId)
                {
                    existingOrder = order;
                    break;
                }
            }

            if (existingOrder != null)
            {
                bool productExistsInOrder = false;
                foreach (Product productInOrder in existingOrder.OrderedProducts)
                {
                    if (productInOrder.ProductId == selectedProduct.ProductId)
                    {
                        productInOrder.Quantity += quantityInt;
                        productExistsInOrder = true;
                        break;
                    }
                }

                if (!productExistsInOrder)
                {
                    existingOrder.OrderedProducts.Add(selectedProduct);
                }
                int orderspro = 1;

                if (int.TryParse(productsOrder.Text, out orderspro))
                {
                    orderspro++;
                }

                productsOrder.Text = orderspro.ToString(); 
                MessageBox.Show("Product added to existing order.");
            }
            else
            {
                products.Add(selectedProduct);

                PurchaseOrder newOrder = new PurchaseOrder(orderId, supplier, status, products);

                PurchaseOrder.Add(newOrder);

                MessageBox.Show("New order created successfully");

                productsOrder.Text = "1";
            }
        }
        private void DoneButton_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Are You Sure", "", MessageBoxButtons.YesNo);
            if (DialogResult == DialogResult.Yes)
            {
                OrderChangeAdd?.Invoke();
                this.Close();
            }
        }
    }
}
