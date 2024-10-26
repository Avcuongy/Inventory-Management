using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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


        private List<string> categories = new List<string>()
    {
        "Phone",
        "Tablet",
        "Keyboard",
        "Mouse",
        "Headphone"
    };
        private DataGridView dataGridView1;

        public Product_Add(Warehouse warehouse, List<Supplier> suppliers, DataGridView dataGridView1)
        {
            InitializeComponent();
            this._warehouse = warehouse;
            this._supplier = suppliers;
            this.dataGridView1 = dataGridView1;
            LoadCategories();
            InitializeTextBoxes();
        }

        private void LoadCategories()
        {
            cBx_Category.Items.Clear();
            cBx_Category.Items.AddRange(categories.ToArray());
            cBx_Category.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void InitializeTextBoxes()
        {
            // Tạo placeholder và chỉ cho phép nhập số cho các TextBox số liệu
            tBx_Price_Product.KeyPress += NumericTextBox_KeyPress;
            tBx_Quantity_Product.Text = "1";

            // Tự động tạo Product ID
            tBx_ID_Product.Text = GenerateNewProductID();
            tBx_ID_Product.ReadOnly = true;
        }

        private string GenerateNewProductID()
        {
            // Tìm ID lớn nhất hiện tại
            int maxID = 0;
            foreach (Product product in _warehouse.Products)
            {
                if (product.ProductId.StartsWith("P"))
                {
                    if (int.TryParse(product.ProductId.Substring(1), out int id))
                    {
                        maxID = Math.Max(maxID, id);
                    }
                }
            }
            return $"P{maxID + 1}";
        }

        private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép nhập số và điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Chỉ cho phép một dấu thập phân
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(tBx_Name_Product.Text))
            {
                MessageBox.Show("Please enter product name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cBx_Category.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a category", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(tBx_Price_Product.Text))
            {
                MessageBox.Show("Please enter price", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(tBx_Supplier_Product.Text))
            {
                MessageBox.Show("Please enter supplier name", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(tBx_Price_Product.Text, out double price) || price < 0)
            {
                MessageBox.Show("Invalid price value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(tBx_Quantity_Product.Text, out int quantity) || quantity < 1)
            {
                MessageBox.Show("Invalid quantity value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Tạo product mới dựa trên category được chọn
                string selectedCategory = cBx_Category.SelectedItem.ToString();
                Product newProduct = null;

                switch (selectedCategory)
                {
                    case "Phone":
                        newProduct = new Phone(tBx_ID_Product.Text, tBx_Name_Product.Text, selectedCategory, 1, price);
                        break;
                    case "Tablet":
                        newProduct = new Tablet(tBx_ID_Product.Text, tBx_Name_Product.Text, selectedCategory, 1, price);
                        break;
                    case "Keyboard":
                        newProduct = new Keyboard(tBx_ID_Product.Text, tBx_Name_Product.Text, selectedCategory, 1, price);
                        break;
                    case "Mouse":
                        newProduct = new Mouse(tBx_ID_Product.Text, tBx_Name_Product.Text, selectedCategory, 1, price);
                        break;
                    case "Headphone":
                        newProduct = new Headphone(tBx_ID_Product.Text, tBx_Name_Product.Text, selectedCategory, 1, price);
                        break;
                }
                if (newProduct != null)
                {
                    // Add product to warehouse
                    _warehouse.Products.Add(newProduct);

                    // Get supplier name from input
                    string supplierName = tBx_Supplier_Product.Text.Trim();

                    // Find appropriate supplier based on category and name
                    Supplier matchingSupplier = null;

                    // Define supplier matching rules based on category
                    switch (selectedCategory)
                    {
                        case "Phone" when supplierName.Equals("Cellphones", StringComparison.OrdinalIgnoreCase):
                        case "Phone" when supplierName.Equals("Samsung", StringComparison.OrdinalIgnoreCase):
                        case "Tablet" when supplierName.Equals("Xiaomi", StringComparison.OrdinalIgnoreCase):
                        case "Headphone" when supplierName.Equals("XuanVuAudio", StringComparison.OrdinalIgnoreCase):
                        case "Headphone" when supplierName.Equals("Sony", StringComparison.OrdinalIgnoreCase):
                        case "Mouse" when supplierName.Equals("Logitech", StringComparison.OrdinalIgnoreCase):
                        case "Mouse" when supplierName.Equals("Razer", StringComparison.OrdinalIgnoreCase):
                        case "Keyboard" when supplierName.Equals("Logitech", StringComparison.OrdinalIgnoreCase):
                        case "Keyboard" when supplierName.Equals("Razer", StringComparison.OrdinalIgnoreCase):
                        case "Tablet" when supplierName.Equals("Microsoft", StringComparison.OrdinalIgnoreCase):
                            matchingSupplier = _supplier.FirstOrDefault(s => s.Name.Equals(supplierName, StringComparison.OrdinalIgnoreCase));
                            break;
                        default:
                            MessageBox.Show("Invalid supplier for this category!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    if (matchingSupplier == null)
                    {
                        // Create new supplier if not exists
                        matchingSupplier = new Supplier(
                            GenerateNewSupplierId(),
                            supplierName,
                            "N/A", // Empty contact info
                            new List<Product> { newProduct }
                        );
                        _supplier.Add(matchingSupplier);
                    }
                    else
                    {
                        // Add product to existing supplier's product list
                        if (matchingSupplier.SuppliedProducts == null)
                        {
                            matchingSupplier.SuppliedProducts = new List<Product>();
                        }

                        // Check if product already exists in supplier's list
                        if (!matchingSupplier.SuppliedProducts.Any(p => p.ProductId == newProduct.ProductId))
                        {
                            matchingSupplier.SuppliedProducts.Add(newProduct);
                        }
                    }

                    // Update UI
                    UpdateDataGridView();
                    ClearInputs();
                    MessageBox.Show("Product added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private string GenerateNewSupplierId()
        {
            // Find maximum supplier ID
            int maxID = 0;
            foreach (Supplier supplier in _supplier)
            {
                if (supplier.SupplierId.StartsWith("S"))
                {
                    if (int.TryParse(supplier.SupplierId.Substring(1), out int id))
                    {
                        maxID = Math.Max(maxID, id);
                    }
                }
            }
            return $"S{maxID + 1}";
        }
        private void UpdateDataGridView()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _warehouse.Products;
            dataGridView1.DataSource = _supplier;
            dataGridView1.Refresh();
        }

        private void ClearInputs()
        {
            tBx_ID_Product.Text = GenerateNewProductID();
            tBx_Name_Product.Clear();
            tBx_Price_Product.Clear();
            cBx_Category.SelectedIndex = -1;
            tBx_Supplier_Product.Clear();
        }
    }
}
