﻿using System;
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
    public partial class Order_Menu : Form
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

        public Order_Menu(string username,
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
            ShowOrders();
        }

        public void ShowOrders()
        {
            List<Supplier> suppliers = _supplier;
            OrderManager orderManager = _orderManager;
            List<PurchaseOrder> purchaseOrders = _orderManager.Orders;
            List<Product> products = _warehouse.Products;

            DataTable dt = new DataTable();

            dt.Columns.Add("Order ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Quantity Order");
            dt.Columns.Add("Total");
            dt.Columns.Add("Status");

            foreach (PurchaseOrder order in purchaseOrders)
            {
                string supplierName = order.Supplier.Name;

                foreach (Product product in order.OrderedProducts)
                {
                    DataRow row = dt.NewRow();
                    row["Order ID"] = order.OrderId;
                    row["Supplier Name"] = supplierName;
                    row["Product ID"] = product.ProductId;
                    row["Product Name"] = product.Name;
                    row["Quantity Order"] = product.Quantity;
                    foreach (Product product2 in products)
                    {
                        if (product.ProductId == product2.ProductId)
                        {
                            row["Total"] = product2.Price * product.Quantity;
                            break;
                        }
                    }
                    row["Status"] = order.Status;

                    dt.Rows.Add(row);
                }
            }

            ShowOrder.DataSource = dt;
        }

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

        private void button3_Click(object sender, EventArgs e)
        {
            ShowOrder.CurrentCell = null;

            List<Supplier> suppliers = _supplier;
            OrderManager orderManager = _orderManager;
            List<PurchaseOrder> purchaseOrders = _orderManager.Orders;
            List<Product> products = _warehouse.Products;

            string id = textBoxSupplier.Text.Trim();

            DataTable dt = new DataTable();

            dt.Columns.Add("Order ID");
            dt.Columns.Add("Supplier Name");
            dt.Columns.Add("Product ID");
            dt.Columns.Add("Product Name");
            dt.Columns.Add("Quantity Order");
            dt.Columns.Add("Total");
            dt.Columns.Add("Status");

            if (string.IsNullOrEmpty(id))
            {
                foreach (PurchaseOrder order in purchaseOrders)
                {
                    string supplierName = order.Supplier.Name;

                    foreach (Product product in order.OrderedProducts)
                    {
                        DataRow row = dt.NewRow();
                        row["Order ID"] = order.OrderId;
                        row["Supplier Name"] = supplierName;
                        row["Product ID"] = product.ProductId;
                        row["Product Name"] = product.Name;
                        row["Quantity Order"] = product.Quantity;

                        foreach (Product product2 in products)
                        {
                            if (product.ProductId == product2.ProductId)
                            {
                                row["Total"] = product2.Price * product.Quantity;
                                break;
                            }
                        }

                        row["Status"] = order.Status;

                        dt.Rows.Add(row);
                    }
                }
            }
            else
            {
                foreach (PurchaseOrder order in purchaseOrders)
                {
                    if (order.OrderId == id || order.Supplier.SupplierId == id || order.Supplier.Name == id)
                    {
                        string supplierName = order.Supplier.Name;

                        foreach (Product product in order.OrderedProducts)
                        {
                            DataRow row = dt.NewRow();
                            row["Order ID"] = order.OrderId;
                            row["Supplier Name"] = supplierName;
                            row["Product ID"] = product.ProductId;
                            row["Product Name"] = product.Name;
                            row["Quantity Order"] = product.Quantity;

                            foreach (Product product2 in products)
                            {
                                if (product.ProductId == product2.ProductId)
                                {
                                    row["Total"] = product2.Price * product.Quantity;
                                    break;
                                }
                            }

                            row["Status"] = order.Status;

                            dt.Rows.Add(row);
                        }
                        break;
                    }
                }
            }

            ShowOrder.DataSource = dt;
        }
    }
}
