﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory_Management
{
    public partial class Profile : Form
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

        public Profile(string username,
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
            ShowInfoInProfile();
        }

        public void ShowInfoInProfile()
        {
            foreach (Employee employ in _warehouse.Employees)
            {
                if (employ.Username == _username)
                {
                    NameLabel.Text = employ.Name;
                    RoleText.Text = employ.Role;
                    EmployeeIDText.Text = employ.EmployeeId;
                    break;
                }
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {           
            DataWrapper dataWrapper = new DataWrapper
            {
                Warehouse = _warehouse,
                Suppliers = _supplier,
                PurchaseOrders = _purchaseOrder,
                ReturnOrders = _returnOrder,
                Customers = _customer,
                OrderManager = _orderManager,
                SalesInvoices = _salesInvoice,
                Report = _report
            };

            string filePath = "Inventory_Management.dat";

            string fileJson = JsonSerializer.Serialize(dataWrapper, new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(filePath, fileJson);

            Environment.Exit(0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Warehouse_Menu warehouse_Menu = new Warehouse_Menu(
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
            this.Hide();
            warehouse_Menu.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Order_Menu order_Menu = new Order_Menu(
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
            order_Menu.Show();
            this.Hide();
        }
    }
}
