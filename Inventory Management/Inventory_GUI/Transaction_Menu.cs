using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory_Management
{
    public partial class Transaction_Menu : Form
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

        private bool ChangeDataView = false;
        public string Username { get => _username; set => _username = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public List<Supplier> Supplier { get => _supplier; set => _supplier = value; }
        public List<PurchaseOrder> PurchaseOrder { get => _purchaseOrder; set => _purchaseOrder = value; }
        public List<ReturnOrder> ReturnOrder { get => _returnOrder; set => _returnOrder = value; }
        public List<Customer> Customer { get => _customer; set => _customer = value; }
        public OrderManager OrderManager { get => _orderManager; set => _orderManager = value; }
        public List<SalesInvoice> SalesInvoice { get => _salesInvoice; set => _salesInvoice = value; }
        public Report Report { get => _report; set => _report = value; }

        public Transaction_Menu(string username,
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
            ShowInvoiceInfo();
        }
        public void ShowInvoiceInfo()
        {
            dGV_Transaction.CurrentCell = null;

            List<SalesInvoice> salesInvoices = SalesInvoice;

            DataTable dt = new DataTable();

            dt.Columns.Add("Invoice ID");
            dt.Columns.Add("Customer ID");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Paid");
            dt.Columns.Add("Status");

            foreach (SalesInvoice salesInvoices1 in SalesInvoice)
            {
                foreach (Product product in salesInvoices1.SoldProducts)
                {
                    DataRow row = dt.NewRow();
                    row["Invoice ID"] = salesInvoices1.InvoiceId;
                    row["Customer ID"] = salesInvoices1.Customer.CustomerId;
                    row["Customer Name"] = salesInvoices1.Customer.Name;
                    row["Product ID"] = product.ProductId;
                    row["Product Name"] = product.Name;
                    row["Category"] = product.Category;
                    row["Quantity"] = product.Quantity;
                    row["Paid"] = product.Price;
                    row["Status"] = salesInvoices1.PaymentStatus;

                    dt.Rows.Add(row);
                }
            }

            dGV_Transaction.DataSource = dt;
        }

        private void ShowReturnInfo()
        {
            List<ReturnOrder> returnOrders = ReturnOrder;

            DataTable dt = new DataTable();

            dGV_Transaction.CurrentCell = null;

            dt.Columns.Add("Return ID");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Refund");
            dt.Columns.Add("Reason");
            dt.Columns.Add("Date");
            dt.Columns.Add("Status");

            foreach (ReturnOrder returnOrder1 in ReturnOrder)
            {
                DataRow row = dt.NewRow();
                row["Return ID"] = returnOrder1.ReturnOrderId;
                row["Product ID"] = returnOrder1.Product.ProductId;
                row["Product Name"] = returnOrder1.Product.Name;
                row["Category"] = returnOrder1.Product.Category;
                row["Quantity"] = returnOrder1.Product.Quantity;
                row["Refund"] = returnOrder1.Product.Price;
                row["Reason"] = returnOrder1.Reason;
                row["Date"] = returnOrder1.ReturnDate.ToString("dd/MM/yyyy");
                row["Status"] = returnOrder1.Status;

                dt.Rows.Add(row);
            }

            dGV_Transaction.DataSource = dt;
        }

        private void ShowPurchaseInfo()
        {
            dGV_Transaction.CurrentCell = null;

            List<PurchaseOrder> purchaseOrders = PurchaseOrder;

            DataTable dt = new DataTable();

            dt.Columns.Add("Purchase ID");
            dt.Columns.Add("Supplier ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Paid");
            dt.Columns.Add("Status");

            foreach (PurchaseOrder purchaseOrders1 in PurchaseOrder)
            {
                foreach (Product product in purchaseOrders1.OrderedProducts)
                {
                    DataRow row = dt.NewRow();
                    row["Purchase ID"] = purchaseOrders1.OrderId;
                    row["Supplier ID"] = purchaseOrders1.Supplier.SupplierId;
                    row["Supplier Name"] = purchaseOrders1.Supplier.Name;
                    row["Product ID"] = product.ProductId;
                    row["Product Name"] = product.Name;
                    row["Category"] = product.Category;
                    row["Quantity"] = product.Quantity;
                    row["Paid"] = product.Price;
                    row["Status"] = purchaseOrders1.Status;

                    dt.Rows.Add(row);
                }
            }

            dGV_Transaction.DataSource = dt;
        }

        private void Return_Order_Click(object sender, EventArgs e)
        {

            button_Return_Order.Text = "Return Order";
            ShowInvoiceInfo();
        }
        private void salesinvoice_Click(object sender, EventArgs e)
        {
            button_Return_Order.Text = "Invoice";
            ShowReturnInfo();
        }
        private void return_Profile_Click(object sender, EventArgs e)
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

        private void button_Search_Click(object sender, EventArgs e)
        {
            dGV_Transaction.CurrentCell = null;

            List<SalesInvoice> salesInvoices = SalesInvoice;
            List<Customer> customers = Customer;
            List<PurchaseOrder> purchaseOrders = PurchaseOrder;
            List<ReturnOrder> returnOrders = ReturnOrder;

            string search = tBx_Search.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                ShowInvoiceInfo();
                return;
            }

            DataTable dt = new DataTable();

            bool found = false;
            dt.Columns.Add("Invoice ID");
            dt.Columns.Add("Customer ID");
            dt.Columns.Add("Customer Name");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Category");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Paid");
            dt.Columns.Add("Status");

            foreach (SalesInvoice invoice in salesInvoices)
            {
                if (invoice.InvoiceId.ToLower() == search ||
                    invoice.Customer.CustomerId.ToLower() == search ||
                    invoice.Customer.Name.ToLower() == search)
                {
                    foreach (Product product in invoice.SoldProducts)
                    {
                        DataRow row = dt.NewRow();
                        row["Invoice ID"] = invoice.InvoiceId;
                        row["Customer ID"] = invoice.Customer.CustomerId;
                        row["Customer Name"] = invoice.Customer.Name;
                        row["Product ID"] = product.ProductId;
                        row["Product Name"] = product.Name;
                        row["Category"] = product.Category;
                        row["Quantity"] = product.Quantity;
                        row["Paid"] = product.Price;
                        row["Status"] = invoice.PaymentStatus;

                        dt.Rows.Add(row);
                        found = true;
                    }
                    break;
                }
            }

            if (!found)
            {
                dt.Clear();
                dt.Columns.Clear();
                dt.Columns.Add("Invoice ID");
                dt.Columns.Add("Supplier ID");
                dt.Columns.Add("Supplier Name");
                dt.Columns.Add("Product ID");
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Category");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Paid");
                dt.Columns.Add("Status");

                foreach (PurchaseOrder order in purchaseOrders)
                {
                    if (order.OrderId.ToLower() == search ||
                        order.Supplier.Name.ToLower() == search ||
                        order.Supplier.SupplierId.ToLower() == search)
                    {
                        foreach (Product product in order.OrderedProducts)
                        {
                            DataRow row = dt.NewRow();
                            row["Invoice ID"] = order.OrderId;
                            row["Supplier ID"] = order.Supplier.SupplierId;
                            row["Supplier Name"] = order.Supplier.Name;
                            row["Product ID"] = product.ProductId;
                            row["Product Name"] = product.Name;
                            row["Category"] = product.Category;
                            row["Quantity"] = product.Quantity;
                            row["Paid"] = product.Price;
                            row["Status"] = order.Status;

                            dt.Rows.Add(row);
                            found = true;
                        }
                        break;
                    }
                }
            }

            if (!found)
            {
                dt.Clear();
                dt.Columns.Clear();
                dt.Columns.Add("Return ID");
                dt.Columns.Add("Product ID");
                dt.Columns.Add("Product Name");
                dt.Columns.Add("Category");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("Refund");
                dt.Columns.Add("Reason");
                dt.Columns.Add("Date");
                dt.Columns.Add("Status");

                foreach (ReturnOrder returnOrder in returnOrders)
                {
                    if (returnOrder.ReturnOrderId.ToLower() == search ||
                        returnOrder.Product.ProductId.ToLower() == search ||
                        returnOrder.Product.Name.ToLower() == search)
                    {
                        DataRow row = dt.NewRow();
                        row["Return ID"] = returnOrder.ReturnOrderId;
                        row["Product ID"] = returnOrder.Product.ProductId;
                        row["Product Name"] = returnOrder.Product.Name;
                        row["Category"] = returnOrder.Product.Category;
                        row["Quantity"] = returnOrder.Product.Quantity;
                        row["Refund"] = returnOrder.Product.Price;
                        row["Reason"] = returnOrder.Reason;
                        row["Date"] = returnOrder.ReturnDate.ToString("dd/MM/yyyy");
                        row["Status"] = returnOrder.Status;

                        dt.Rows.Add(row);
                        found = true;
                        break;
                    }
                }
            }

            dGV_Transaction.DataSource = dt;
        }

        private void button_Supplier_Transaction_Click(object sender, EventArgs e)
        {
            ShowPurchaseInfo();
        }
        private void UpdateInvoiceStatus(string id, string newStatus)
        {
            if (newStatus != "Done" && newStatus != "Processing" && newStatus != "Pending")
            {
                MessageBox.Show("Not Found Status");
                return;
            }

            bool found = false;

            foreach (SalesInvoice invoice in SalesInvoice)
            {
                if (invoice.InvoiceId == id)
                {
                    invoice.PaymentStatus = newStatus;
                    found = true;
                    ShowInvoiceInfo();
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Error");
            }
        }
        private void UpdateReturnStatus(string id, string newStatus)
        {
            if (newStatus != "Done" && newStatus != "Processing" && newStatus != "Pending")
            {
                MessageBox.Show("Not Found Status");
                return;
            }

            bool found = false;

            foreach (ReturnOrder returnOrder in ReturnOrder)
            {
                if (returnOrder.ReturnOrderId == id)
                {
                    returnOrder.Status = newStatus;
                    found = true;
                    ShowReturnInfo();
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Error");
            }
        }
        private void UpdatePurchaseStatus(string id, string newStatus)
        {
            if (newStatus != "Done" && newStatus != "Processing" && newStatus != "Pending")
            {
                MessageBox.Show("Not Found Status");
                return;
            }

            bool found = false;

            foreach (PurchaseOrder order in PurchaseOrder)
            {
                if (order.OrderId == id)
                {
                    order.Status = newStatus;
                    found = true;
                    ShowPurchaseInfo();
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Error");
            }
        }
        private void dGV_Transaction_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int statusColumnIndex = dGV_Transaction.Columns["Status"].Index;

            DialogResult = MessageBox.Show("Are You Sure", "", MessageBoxButtons.YesNo);

            if (DialogResult == DialogResult.Yes)
            {
                if (e.ColumnIndex == statusColumnIndex)
                {
                    string newStatus = dGV_Transaction.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

                    DataGridViewRow row = dGV_Transaction.Rows[e.RowIndex];

                    if (dGV_Transaction.Columns["Invoice ID"] != null)
                    {
                        string invoiceId = dGV_Transaction.Rows[e.RowIndex].Cells["Invoice ID"].Value.ToString();
                        if (!string.IsNullOrEmpty(invoiceId))
                        {
                            UpdateInvoiceStatus(invoiceId, newStatus);
                            return;
                        }
                    }

                    if (dGV_Transaction.Columns["Purchase ID"] != null)
                    {
                        string purchaseId = dGV_Transaction.Rows[e.RowIndex].Cells["Purchase ID"].Value.ToString();
                        if (!string.IsNullOrEmpty(purchaseId))
                        {
                            UpdatePurchaseStatus(purchaseId, newStatus);
                            return;
                        }
                    }

                    if (dGV_Transaction.Columns["Return ID"] != null)
                    {
                        string returnId = dGV_Transaction.Rows[e.RowIndex].Cells["Return ID"].Value.ToString();
                        if (!string.IsNullOrEmpty(returnId))
                        {
                            UpdateReturnStatus(returnId, newStatus);
                            return;
                        }
                    }
                }
            }
        }


    }
}
