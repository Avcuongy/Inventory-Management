using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management
{
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
            InitializeDataGridView();
            LoadProductData();
        }
        private void InitializeDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;

            //Them cot
            dataGridView1.Columns.Add("ProductId", "ID");
            dataGridView1.Columns.Add("Name", "Product Name");
            dataGridView1.Columns.Add("Category", "Category");
            dataGridView1.Columns.Add("Price", "Price");
            dataGridView1.Columns.Add("Supplier", "Supplier");

            //Thuoc tinh cot
            dataGridView1.Columns["ProductId"].DataPropertyName = "ProductId";
            dataGridView1.Columns["Name"].DataPropertyName = "Name";
            dataGridView1.Columns["Category"].DataPropertyName = "Category";
            dataGridView1.Columns["Price"].DataPropertyName = "Price";
            dataGridView1.Columns["Supplier"].DataPropertyName = "Supplier";
        }
        private void RefreshDataGridView()
        {
            DataTable displayData = new DataTable();

            displayData.Columns.Add("ProductId", typeof(string));
            displayData.Columns.Add("Name", typeof(string));
            displayData.Columns.Add("Category", typeof(string));
            displayData.Columns.Add("Price", typeof(decimal));
            displayData.Columns.Add("Supplier", typeof(string));

            for (int i = 0; i < Warehouse.Products.Count; i++)
            {
                Product product = Warehouse.Products[i];
                string supplierName = GetSupplierName(product.ProductId);

                DataRow row = displayData.NewRow();
                row["ProductId"] = product.ProductId;
                row["Name"] = product.Name;
                row["Category"] = product.Category;
                row["Price"] = product.Price;
                row["Supplier"] = supplierName;

                displayData.Rows.Add(row);
            }

            dataGridView1.DataSource = displayData;
        }
        private string GetSupplierName(string productId)
        {
            string supplierName = "N/A";

            for (int i = 0; i < Supplier.Count; i++)
            {
                List<Product> supplierProducts = Supplier[i].SuppliedProducts;
                for (int j = 0; j < supplierProducts.Count; j++)
                {
                    if (supplierProducts[j].ProductId == productId)
                    {
                        supplierName = Supplier[i].Name;
                        return supplierName;
                    }
                }
            }
            return supplierName;
        }
        private void LoadProductData()
        {
            if (Warehouse != null && Warehouse.Products != null)
            {
                Products = Warehouse.Products;
                RefreshDataGridView();
            }
            else
            {
                Products = new List<Product>();
                MessageBox.Show("No products found in _warehouse.");
            }
        }
        public void AddProduct(Product product)
        {
            Products.Add(product);
            Warehouse.Products = Products;
            RefreshDataGridView();
        }
        public void UpdateProduct(Product product)
        {
            int index = -1;
            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].ProductId == product.ProductId)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                Products[index] = product;
                Warehouse.Products = Products;
                RefreshDataGridView();
            }
        }
        private void tbx_Search_Product_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();
            List<Product> filteredProducts = new List<Product>();

            for (int i = 0; i < Products.Count; i++)
            {
                if (Products[i].Name.ToLower().Contains(searchText) ||
                    Products[i].ProductId.ToLower().Contains(searchText))
                {
                    filteredProducts.Add(Products[i]);
                }
            }
            DataTable displayData = new DataTable();
            displayData.Columns.Add("ProductId", typeof(string));
            displayData.Columns.Add("Name", typeof(string));
            displayData.Columns.Add("Category", typeof(string));
            displayData.Columns.Add("Price", typeof(decimal));
            displayData.Columns.Add("Supplier", typeof(string));

            for (int i = 0; i < filteredProducts.Count; i++)
            {
                Product product = filteredProducts[i];
                string supplierName = GetSupplierName(product.ProductId);

                DataRow row = displayData.NewRow();
                row["ProductId"] = product.ProductId;
                row["Name"] = product.Name;
                row["Category"] = product.Category;
                row["Price"] = product.Price;
                row["Supplier"] = supplierName;

                displayData.Rows.Add(row);
            }
            dataGridView1.DataSource = displayData;
        }

        private void button_Add_Product_Click_1(object sender, EventArgs e)
        {
            // Tạo form thêm sản phẩm mới
            using (Product_Add addProductForm = new Product_Add(Warehouse, Supplier, dataGridView1))
            {
                // Hiển thị form dạng dialog
                DialogResult result = addProductForm.ShowDialog();

                // Nếu form đóng bình thường (không phải do lỗi)
                if (result == DialogResult.OK || result == DialogResult.Cancel)
                {
                    // Cập nhật lại DataGridView
                    LoadProductData();
                }
            }
        }
        private void button_Update_Product_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có sản phẩm nào trong danh sách không
                if (Products.Count == 0)
                {
                    MessageBox.Show("No products available to update.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo instance của form Product_Update
                Product_Update updateProductForm = new Product_Update(Username, Warehouse, Supplier, PurchaseOrder, ReturnOrder, Customer, OrderManager, SalesInvoice, Report);

                // Đăng ký event handler để nhận thông tin sản phẩm đã được cập nhật
                updateProductForm.ProductUpdated += (Product updatedProduct) =>
                {
                    UpdateProduct(updatedProduct);
                };

                // Hiển thị form dạng dialog
                updateProductForm.ShowDialog();

                // Sau khi form đóng, refresh lại DataGridView
                RefreshDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening update form: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            profileForm.Show();
            this.Close();
        }
    }
}
