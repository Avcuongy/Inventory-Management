using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Inventory_Management.Inventory_GUI
{
    public partial class Analysis_Menu : Form
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

        public Analysis_Menu(string username,
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

            Series series = new Series("Total Price");
            series.ChartType = SeriesChartType.Column; // Đặt kiểu đồ thị là cột
            chart_Analysis.Series.Add(series); // Thêm series vào chart

            if (chart_Analysis.ChartAreas.Count == 0)
            {
                ChartArea chartArea = new ChartArea("SalesArea");
                chart_Analysis.ChartAreas.Add(chartArea);
            }

            foreach (SalesInvoice salesInvoices in SalesInvoice)
            {
                foreach (Product product in salesInvoices.SoldProducts)
                {
                    chart_Analysis.Series["Total Price"].Points.AddXY(product.Name, product.Price);
                }
            }
            chart_Analysis.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            chart_Analysis.ChartAreas[0].AxisX.Interval = 1;
            chart_Analysis.ChartAreas[0].AxisY.Title = "Price";
            chart_Analysis.ChartAreas[0].AxisY.Maximum = Double.NaN; // Tự động điều chỉnh
            chart_Analysis.ChartAreas[0].RecalculateAxesScale(); // Tính toán lại tỷ lệ các trục
            chart_Analysis.ChartAreas[0].AxisX.Title = "Product Name";

            // Set chart title
            Title title = new Title("Product Sales Report");
            title.Font = new Font("Righteous", 16, FontStyle.Bold);
            chart_Analysis.Titles.Add(title);
        }

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

        private void button_Supplier_Chart_Click(object sender, EventArgs e)
        {
            List<PurchaseOrder> purchase = PurchaseOrder;
            List<SalesInvoice> salesInvoices = SalesInvoice;

            chart_Analysis.Series.Clear();
            chart_Analysis.Titles.Clear();

            if (!ChangeDataView)
            {
                button_Supplier_Chart.Text = "Supplier";

                Series series = new Series("Total Price");
                series.ChartType = SeriesChartType.Column; // Đặt kiểu đồ thị là cột
                chart_Analysis.Series.Add(series); // Thêm series vào chart

                if (chart_Analysis.ChartAreas.Count == 0)
                {
                    ChartArea chartArea = new ChartArea("SalesArea");
                    chart_Analysis.ChartAreas.Add(chartArea);
                }

                Dictionary<string, double> supplierTotals = new Dictionary<string, double>();

                // Duyệt qua từng đơn hàng
                for (int i = 0; i < PurchaseOrder.Count; i++)
                {
                    string supplierName = PurchaseOrder[i].Supplier.Name;
                    double orderTotal = 0;

                    // Duyệt qua từng sản phẩm trong đơn hàng
                    for (int j = 0; j < PurchaseOrder[i].OrderedProducts.Count; j++)
                    {
                        // Tính giá trị sản phẩm = giá * số lượng
                        double productValue = PurchaseOrder[i].OrderedProducts[j].Price;
                        orderTotal += productValue;
                    }

                    // Cộng dồn vào tổng giá trị của nhà cung cấp
                    if (supplierTotals.ContainsKey(supplierName))
                    {
                        supplierTotals[supplierName] += orderTotal;
                    }
                    else
                    {
                        supplierTotals.Add(supplierName, orderTotal);
                    }
                }

                foreach (KeyValuePair<string, double> supplier in supplierTotals)
                {
                    int pointIndex = series.Points.AddXY(supplier.Key, supplier.Value);
                    series.Points[pointIndex].Label = supplier.Value.ToString("N0");
                }
                chart_Analysis.Update();
                chart_Analysis.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chart_Analysis.ChartAreas[0].AxisX.Interval = 1;
                chart_Analysis.ChartAreas[0].AxisY.Title = "Price";
                chart_Analysis.ChartAreas[0].AxisY.Maximum = Double.NaN; // Tự động điều chỉnh
                chart_Analysis.ChartAreas[0].RecalculateAxesScale(); // Tính toán lại tỷ lệ các trục
                chart_Analysis.ChartAreas[0].AxisX.Title = "Supplier Name";

                Title title = new Title("Supplier Transaction Report");
                title.Font = new Font("Righteous", 16, FontStyle.Bold);
                chart_Analysis.Titles.Add(title);
            }
            else
            {
                button_Supplier_Chart.Text = "Product";

                Series series = new Series("Total Price");
                series.ChartType = SeriesChartType.Column;
                chart_Analysis.Series.Add(series);

                if (chart_Analysis.ChartAreas.Count == 0)
                {
                    ChartArea chartArea = new ChartArea("SalesArea");
                    chart_Analysis.ChartAreas.Add(chartArea);
                }

                foreach (SalesInvoice salesInvoices1 in SalesInvoice)
                {
                    foreach (Product product in salesInvoices1.SoldProducts)
                    {
                        chart_Analysis.Series["Total Price"].Points.AddXY(product.Name, product.Price);
                    }
                }
                chart_Analysis.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
                chart_Analysis.ChartAreas[0].AxisX.Interval = 1;
                chart_Analysis.ChartAreas[0].AxisY.Title = "Price";
                chart_Analysis.ChartAreas[0].AxisY.Maximum = Double.NaN; // Tự động điều chỉnh
                chart_Analysis.ChartAreas[0].RecalculateAxesScale(); // Tính toán lại tỷ lệ các trục
                chart_Analysis.ChartAreas[0].AxisX.Title = "Product Name";

                Title title = new Title("Product Sales Report");
                title.Font = new Font("Righteous", 16, FontStyle.Bold);
                chart_Analysis.Titles.Add(title);
            }
            ChangeDataView = !ChangeDataView;

        }
    }
}
