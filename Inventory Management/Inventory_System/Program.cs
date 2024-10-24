using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using System.Drawing;

namespace Inventory_Management
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Khởi tạo các đối tượng
            Warehouse warehouse = new Warehouse();
            List<Supplier> suppliers = new List<Supplier>();
            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();
            List<ReturnOrder> returnOrders = new List<ReturnOrder>();
            List<Customer> customers = new List<Customer>();
            OrderManager orderManager = new OrderManager(purchaseOrders);
            List<SalesInvoice> salesInvoices = new List<SalesInvoice>();
            Report report = new Report(salesInvoices);

            string filePath = "Inventory_Management.dat";

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);

                DataWrapper dataWrapper = JsonSerializer.Deserialize<DataWrapper>(fileContent);

                // Gán dữ liệu từ dataWrapper vào các biến tương ứng
                warehouse = dataWrapper.Warehouse;
                suppliers = dataWrapper.Suppliers;
                purchaseOrders = dataWrapper.PurchaseOrders;
                returnOrders = dataWrapper.ReturnOrders;
                customers = dataWrapper.Customers;
                orderManager = dataWrapper.OrderManager;
                salesInvoices = dataWrapper.SalesInvoices;
                report = dataWrapper.Report;
            }
            else
            {
                // Warehouse
                warehouse.Products = new List<Product>
                {
                    new Phone("P1", "Iphone 16 Pro", "Phone", 1, 199),
                    new Phone("P2", "One Plus Ace 3v", "Phone", 1, 299),
                    new Tablet("P3", "Xiaomi pad 6", "Tablet", 1, 399),
                    new Keyboard("P4", "Cidoo", "Keyboard", 1, 890),
                    new Headphone("P5", "Tanjim Zero", "Headphone", 1, 199),

                    new Mouse("P6", "G304", "Mouse", 1, 490),
                    new Phone("P7", "Iphone 16 Pro Plus", "Phone", 1, 390),
                    new Tablet("P8", "Ipad Gen 6", "Tablet", 1, 149),
                    new Keyboard("P9", "Logitech K102", "Keyboard", 1, 690),
                    new Headphone("P10", "Moondrop Jiu", "Headphone", 1, 299)
                };

                warehouse.Employees = new List<Employee>
                {
                    new Employee("E1", "Tommy James", "Employee", "U1", "P1"),
                    new Employee("E2", "Ethan Nick", "Employee", "U2", "P2"),
                    new Employee("E3", "Sofia Briam", "Employee", "U3", "P3"),
                    new Employee("E4", "Liam Philip", "Employee", "U4", "P4"),
                    new Employee("E5", "Micas Sa", "Employee", "U5", "P5"),
                    new Employee("E6", "Luke Hamilton", "Employee", "U6", "P6"),
                    new Employee("E7", "Mason Green", "Employee", "U7", "P7"),
                    new Employee("E8", "Ohio Loto", "Employee", "U8", "P8"),
                    new Employee("E9", "Mia James", "Employee", "U9", "P9"),
                    new Employee("E10", "Mi chot So", "Employee", "U10", "P10")
                };

                warehouse.Inventory = new List<Inventory>
                {
                    new Inventory(new Dictionary<String, int>() {
                        { "P1", 0 },
                        { "P2", 20 },
                        { "P3", 21 },
                        { "P4", 11 },
                        { "P5", 31 },
                        { "P6", 21 },
                        { "P7", 41 },
                        { "P8", 15 },
                        { "P9", 29 },
                        { "P10", 34 }
                })};

                // Supplier
                suppliers = new List<Supplier>()
                {
                new Supplier("S1", "Cellphones", "0912345671",
                new List<Product>()
                {
                    new Phone("P1", "Iphone 16 Pro", "Phone", 1, 199),
                    new Phone("P2", "One Plus Ace 3v", "Phone", 1, 299),
                    new Phone("P7", "Iphone 16 Pro Plus", "Phone", 1, 399),
                    new Keyboard("P4", "Cidoo", "Keyboard", 1, 890),
                    new Mouse("P6", "G304", "Mouse", 1, 490),
                    new Tablet("P8", "Ipad Gen 6", "Tablet", 1, 149),
                    new Keyboard("P9", "Logitech K102", "Keyboard", 1, 690)
                }
                ),

                new Supplier("S2", "Xiaomi", "0987655555",
                new List<Product>()
                {
                    new Tablet("P3", "Xiaomi pad 6", "Tablet", 1, 399)
                }
                ),

                new Supplier("S3", "XuanVuAudio", "1230098006",
                new List<Product>()
                {
                    new Headphone("P5", "Tanjim Zero", "Headphone", 1, 199),
                    new Headphone("P10", "Moondrop Jiu", "Headphone", 1, 299)
                })
            };

                // PurchaseOrder
                purchaseOrders = new List<PurchaseOrder>()
            {
                new PurchaseOrder("PD1",suppliers[0],"Pending",new List<Product>()
                {
                     new Phone("P1", "Iphone 16 Pro", "Phone", 40, 7960),
                     new Tablet("P8", "Ipad Gen 6", "Tablet", 5, 745),
                     new Keyboard("P9", "Logitech K102", "Keyboard", 100, 69000)
                })
                ,
                new PurchaseOrder("PD2",suppliers[1],"Pending",new List<Product>()
                {
                    new Tablet("P3", "Xiaomi pad 6", "Tablet", 10, 3990)
                })
                ,
                new PurchaseOrder("PD3",suppliers[2],"Pending",new List<Product>()
                {
                    new Headphone("P5", "Tanjim Zero", "Headphone", 10, 1990),
                    new Headphone("P10", "Moondrop Jiu", "Headphone", 5, 1495)
                })
            };

                // ReturnOrder
                returnOrders = new List<ReturnOrder>()
            {
                new ReturnOrder("R1",
                new Phone("P1", "Iphone 16 Pro", "Phone", 3, 597),"Broken During Transportation", new DateTime(2024,10,23),"Done"
                    ),
                new ReturnOrder("R2",
                new Headphone("P5", "Tanjim Zero", "Headphone", 2, 398),"Broken During Transportation", new DateTime(2024,10,24),"Done"
                    ),
                new ReturnOrder("R3",
                new Keyboard("P4", "Cidoo", "Keyboard", 5, 4450),"Broken During Transportation", new DateTime(2024,10,25),"Done"
                )
            };

                //Customer
                customers = new List<Customer>()
            {
                new Customer("C1","Viet","00000012345"),
                new Customer("C2","Huy","00234981233"),
                new Customer("C3","Cuong","0334901239")
            };

                // OrderManager
                orderManager = new OrderManager(purchaseOrders);

                // SaleInvoice
                salesInvoices = new List<SalesInvoice>()
            {
                new SalesInvoice("SI01",customers[0],new List<Product>
                {
                     new Phone("P1", "Iphone 16 Pro", "Phone", 1, 199)
                }
                ,"Done"),
                new SalesInvoice("SI02",customers[1],new List<Product>
{
                     new Tablet("P8", "Ipad Gen 6", "Tablet", 2, 298)
                }
                ,"Done")
            };

                // Report
                report = new Report(salesInvoices);
            }

            Login login = new Login(warehouse,
                                    suppliers,
                                    purchaseOrders,
                                    returnOrders,
                                    customers,
                                    orderManager,
                                    salesInvoices,
                                    report);

            Application.Run(login);
        }
    }
}
