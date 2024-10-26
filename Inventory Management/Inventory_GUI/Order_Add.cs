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

            ShowComboBox();
            comboBox11.SelectedIndexChanged += comboBox11_SelectedIndexChanged;
            textBox3.TextChanged += textBox3_TextChanged;
        }

        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

        public void ShowComboBox()
        {
            List<Product> products = Warehouse.Products;
            List<string> productsId = new List<string>();

            foreach (Product product in products)
            {
                productsId.Add(product.ProductId);
            }

            comboBox11.DataSource = productsId;
        }


        public void ShowInfo()
        {
            string productId = comboBox11.SelectedItem?.ToString().Trim();
            if (string.IsNullOrEmpty(productId))
            {
                return;
            }

            Product selectedProduct = GetProductById(productId);
            if (selectedProduct == null)
            {
                return;
            }

            Supplier productSupplier = GetSupplierForProduct(productId);
            if (productSupplier == null)
            {
                return;
            }

            OrderIDText.Text = GetOrderId();
            textBox2.Text = selectedProduct.Name;
            SupplierText.Text = productSupplier.Name;
            textBox3.Text = "1";
            textBox1.Text = selectedProduct.Price.ToString();
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
            return int.TryParse(textBox3.Text, out int quantity) ? quantity : 0;
        }

        private double GetTotalPrice()
        {
            return double.TryParse(textBox1.Text, out double totalPrice) ? totalPrice : 0;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            string selectedProductId = comboBox11.SelectedItem?.ToString();
            int quantity_add_order = GetQuantity();

            Product product = GetProductById(selectedProductId);
            if (product != null)
            {
                textBox1.Text = (quantity_add_order > 0 ? product.Price * quantity_add_order : 0).ToString();
            }
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure ?", "", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string orderId = OrderIDText.Text.Trim();
                string productId = comboBox11.SelectedItem?.ToString();
                string status = comboBox2.SelectedItem?.ToString() ?? "";

                if (string.IsNullOrEmpty(productId) || GetQuantity() <= 0 || string.IsNullOrEmpty(status))
                {
                    MessageBox.Show("An error occurred, please check again");
                    return;
                }

                PurchaseOrder newOrder = new PurchaseOrder(
                    orderId,
                    GetSupplierForProduct(productId),
                    status,
                    new List<Product> { GetProductById(productId) 
                    });

                PurchaseOrder.Add(newOrder);
                MessageBox.Show("Successful");
                this.Close();
            }
        }
    }
}
