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
    public partial class Product_Update : Form
    {
        public delegate void ProductUpdatedHandler(Product product);
        public event ProductUpdatedHandler ProductUpdated;

        private string _username;
        private Warehouse _warehouse;
        private List<Supplier> _supplier = new List<Supplier>();
        private List<PurchaseOrder> _purchaseOrder = new List<PurchaseOrder>();
        private List<ReturnOrder> _returnOrder = new List<ReturnOrder>();
        private List<Customer> _customer = new List<Customer>();
        private OrderManager _orderManager;
        private List<SalesInvoice> _salesInvoice = new List<SalesInvoice>();
        private Report _report;

        public Product_Update(string username, Warehouse warehouse, List<Supplier> suppliers, List<PurchaseOrder> purchaseOrders,
        List<ReturnOrder> returnOrders, List<Customer> customers, OrderManager orderManager,
        List<SalesInvoice> salesInvoices, Report report)
        {
            InitializeComponent();
            this._username = username;
            this._warehouse = warehouse;
            this._supplier = suppliers;
            this._purchaseOrder = purchaseOrders;
            this._returnOrder = returnOrders;
            this._customer = customers;
            this._orderManager = orderManager;
            this._salesInvoice = salesInvoices;
            this._report = report;
            InitializeControls();
            LoadProductIds();
            InitializeEvents();
        }

        private void InitializeControls()
        {
            // Populate category combobox
            cBx_Category.Items.AddRange(new string[] {
            "Tablet",
            "Phone",
            "Mouse",
            "Keyboard",
            "Headphone"
        });
            cBx_Category.DropDownStyle = ComboBoxStyle.DropDownList;

            // Make ID combobox read-only since it's for selection only
            cBx_ID_Product.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadProductIds()
        {
            cBx_ID_Product.Items.Clear();
            foreach (Product product in _warehouse.Products)
            {
                cBx_ID_Product.Items.Add(product.ProductId);
            }
        }

        private void InitializeEvents()
        {
            // Add event handlers
            cBx_ID_Product.SelectedIndexChanged += CBx_ID_Product_SelectedIndexChanged;
            tBx_Price_Product.KeyPress += ValidateDecimalInput;
            tBx_Quantity_Product.KeyPress += ValidateNumberInput;
            button1.Click += Btn_Confirm_Click;
        }

        private void CBx_ID_Product_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cBx_ID_Product.SelectedItem != null)
            {
                string selectedId = cBx_ID_Product.SelectedItem.ToString();

                // Tìm sản phẩm được chọn trong warehouse
                Product selectedProduct = null;
                for (int i = 0; i < _warehouse.Products.Count; i++)
                {
                    if (_warehouse.Products[i].ProductId == selectedId)
                    {
                        selectedProduct = _warehouse.Products[i];
                        break;
                    }
                }

                if (selectedProduct != null)
                {
                    // Tìm supplier chứa sản phẩm này
                    Supplier productSupplier = null;
                    for (int i = 0; i < _supplier.Count; i++)
                    {
                        for (int j = 0; j < _supplier[i].SuppliedProducts.Count; j++)
                        {
                            if (_supplier[i].SuppliedProducts[j].ProductId == selectedId)
                            {
                                productSupplier = _supplier[i];
                                break;
                            }
                        }
                        if (productSupplier != null) break;
                    }

                    // Hiển thị thông tin sản phẩm
                    tBx_Name_Product.Text = selectedProduct.Name;
                    tBx_Quantity_Product.Text = selectedProduct.Quantity.ToString();
                    tBx_Price_Product.Text = selectedProduct.Price.ToString();
                    cBx_Category.SelectedItem = selectedProduct.Category;

                    // Hiển thị tên supplier nếu tìm thấy
                    if (productSupplier != null)
                    {
                        tBx_Supplier_Product.Text = productSupplier.Name;
                    }
                }
            }
        }

        private void ValidateDecimalInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            TextBox textBox = (TextBox)sender;
            if (e.KeyChar == '.' && textBox.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void ValidateNumberInput(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Btn_Confirm_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                UpdateProduct();
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(tBx_Name_Product.Text) ||
                string.IsNullOrWhiteSpace(tBx_Quantity_Product.Text) ||
                string.IsNullOrWhiteSpace(tBx_Supplier_Product.Text) ||
                string.IsNullOrWhiteSpace(tBx_Price_Product.Text) ||
                cBx_Category.SelectedItem == null ||
                cBx_ID_Product.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all required fields.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(tBx_Quantity_Product.Text, out _))
            {
                MessageBox.Show("Please enter a valid quantity.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(tBx_Price_Product.Text, out _))
            {
                MessageBox.Show("Please enter a valid price.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void UpdateProduct()
        {
            try
            {
                string selectedId = cBx_ID_Product.SelectedItem.ToString();
                string newSupplierName = tBx_Supplier_Product.Text.Trim();

                // Find and update product in warehouse
                for (int i = 0; i < _warehouse.Products.Count; i++)
                {
                    if (_warehouse.Products[i].ProductId == selectedId)
                    {
                        _warehouse.Products[i].Name = tBx_Name_Product.Text.Trim();
                        _warehouse.Products[i].Price = double.Parse(tBx_Price_Product.Text);
                        _warehouse.Products[i].Category = cBx_Category.SelectedItem.ToString();
                        break;
                    }
                }

                // Find current supplier of the product and remove product from their list
                for (int i = 0; i < _supplier.Count; i++)
                {
                    for (int j = 0; j < _supplier[i].SuppliedProducts.Count; j++)
                    {
                        if (_supplier[i].SuppliedProducts[j].ProductId == selectedId)
                        {
                            _supplier[i].SuppliedProducts.RemoveAt(j);
                            break;
                        }
                    }
                }

                // Find or create new supplier
                Supplier newSupplier = null;
                for (int i = 0; i < _supplier.Count; i++)
                {
                    if (_supplier[i].Name.ToLower() == newSupplierName.ToLower())
                    {
                        newSupplier = _supplier[i];
                        break;
                    }
                }

                if (newSupplier == null)
                {
                    string newSupplierId = "S" + (_supplier.Count + 1).ToString();
                    newSupplier = new Supplier(newSupplierId, newSupplierName, "", new List<Product>());
                    _supplier.Add(newSupplier);
                }

                // Add product to new supplier's list
                Product updatedProduct = null;
                for (int i = 0; i < _warehouse.Products.Count; i++)
                {
                    if (_warehouse.Products[i].ProductId == selectedId)
                    {
                        updatedProduct = _warehouse.Products[i];
                        break;
                    }
                }
                newSupplier.SuppliedProducts.Add(updatedProduct);

                // Raise the event to notify that a product has been updated
                //OnProductUpdated(updatedProduct);

                MessageBox.Show("Product updated successfully!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating product: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnProductUpdated(Product product)
        {
            ProductUpdated?.Invoke(product);
        }
    }
}
