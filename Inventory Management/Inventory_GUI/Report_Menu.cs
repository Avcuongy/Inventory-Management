using System;
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
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Inventory_Management.Inventory_System;

namespace Inventory_Management
{
    public partial class Report_Menu : Form
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

        public Report_Menu(string username,
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
            ShowChart();
        }
        public void ShowChart()
        {
            List<SalesInvoice> invoices = SalesInvoice;

            chart_Analysis.Series.Clear();

            Series revenueSeries = new Series("Total Amount");
            revenueSeries.ChartType = SeriesChartType.Column;
            chart_Analysis.Series.Add(revenueSeries);

            if (chart_Analysis.ChartAreas.Count == 0)
            {
                ChartArea chartArea = new ChartArea("SalesArea");
                chart_Analysis.ChartAreas.Add(chartArea);
            }

            Dictionary<string, double> productRevenue = new Dictionary<string, double>();

            foreach (SalesInvoice invoice in invoices)
            {
                if (invoice.PaymentStatus == "Paid")
                {
                    foreach (Product product in invoice.SoldProducts)
                    {
                        double productTotal = product.Price * product.Quantity;

                        if (productRevenue.ContainsKey(product.Name))
                        {
                            productRevenue[product.Name] += productTotal;
                        }
                        else
                        {
                            productRevenue.Add(product.Name, productTotal);
                        }
                    }
                }
            }

            foreach (KeyValuePair<string, double> entry in productRevenue)
            {
                int pointIndex = revenueSeries.Points.AddXY(entry.Key, entry.Value);
                revenueSeries.Points[pointIndex].Label = entry.Value.ToString("F0");
            }

            chart_Analysis.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart_Analysis.ChartAreas[0].AxisX.Interval = 1;
            chart_Analysis.ChartAreas[0].AxisX.Title = "Product Name";
            chart_Analysis.ChartAreas[0].AxisY.Title = "Total Amount";

            Title title = new Title("Total Amount by Product");
            title.Font = new Font("Righteous", 14, FontStyle.Bold);
            chart_Analysis.Titles.Add(title);

        }
        // Back Profile
        private void return_Profile_Click_Click(object sender, EventArgs e)
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

        private void Report_Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            DataWarehouse dataWarehouse = new DataWarehouse(Warehouse, Supplier, Customer);
            DataSales dataSales = new DataSales(SalesInvoice, Report);
            DataOrder dataOrder = new DataOrder(PurchaseOrder, ReturnOrder, OrderManager);

            string pathDataWarehouse = "DataWarehouse.dat";
            string pathDataSales = "DataSales.dat";
            string pathDataOrder = "DataOrder.dat";

            string serializedDataWarehouse = JsonSerializer.Serialize(dataWarehouse, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(pathDataWarehouse, serializedDataWarehouse);

            string serializedDataSales = JsonSerializer.Serialize(dataSales, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(pathDataSales, serializedDataSales);

            string serializedDataOrder = JsonSerializer.Serialize(dataOrder, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(pathDataOrder, serializedDataOrder);
        }
    }
}
