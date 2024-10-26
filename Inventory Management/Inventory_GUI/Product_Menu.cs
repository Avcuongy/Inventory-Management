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
            _username = username;
            _warehouse = warehouse;
            _supplier = supplier;
            _purchaseOrder = purchaseOrder;
            _returnOrder = returnOrder;
            _customer = customer;
            _orderManager = orderManager;
            _salesInvoice = salesInvoice;
            _report = report;
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

            for (int i = 0; i < _warehouse.Products.Count; i++)
            {
                Product product = _warehouse.Products[i];
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

            for (int i = 0; i < _supplier.Count; i++)
            {
                List<Product> supplierProducts = _supplier[i].SuppliedProducts;
                for (int j = 0; j < supplierProducts.Count; j++)
                {
                    if (supplierProducts[j].ProductId == productId)
                    {
                        supplierName = _supplier[i].Name;
                        return supplierName;
                    }
                }
            }
            return supplierName;
        }
        private void LoadProductData()
        {
            if (_warehouse != null && _warehouse.Products != null)
            {
                products = _warehouse.Products;
                RefreshDataGridView();
            }
            else
            {
                products = new List<Product>();
                MessageBox.Show("No products found in _warehouse.");
            }
        }
        public void AddProduct(Product product)
        {
            products.Add(product);
            _warehouse.Products = products;
            RefreshDataGridView();
        }
        public void UpdateProduct(Product product)
        {
            int index = -1;
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].ProductId == product.ProductId)
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                products[index] = product;
                _warehouse.Products = products;
                RefreshDataGridView();
            }
        }
        private void tbx_Search_Product_TextChanged(object sender, EventArgs e)
        {
            string searchText = textBox1.Text.ToLower();
            List<Product> filteredProducts = new List<Product>();

            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Name.ToLower().Contains(searchText) ||
                    products[i].ProductId.ToLower().Contains(searchText))
                {
                    filteredProducts.Add(products[i]);
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
            using (Product_Add addProductForm = new Product_Add(_warehouse, _supplier, dataGridView1))
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
                if (products.Count == 0)
                {
                    MessageBox.Show("No products available to update.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tạo instance của form Product_Update
                Product_Update updateProductForm = new Product_Update(_username, _warehouse, _supplier, _purchaseOrder, _returnOrder, _customer, _orderManager, _salesInvoice, _report);

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
            Profile profileForm = new Profile(_username,
                                    _warehouse,
                                    _supplier,
                                    _purchaseOrder,
                                    _returnOrder,
                                    _customer,
                                    _orderManager,
                                    _salesInvoice,
                                    _report);
            profileForm.Show();
            this.Close();
        }
    }
}
