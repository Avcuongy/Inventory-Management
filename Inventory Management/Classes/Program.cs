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

            // Warehouse
            Warehouse warehouse = new Warehouse();

            warehouse.Products.Add(new Phone("P1", "Iphone 16 Pro", "Phone", 40, 199000));
            warehouse.Products.Add(new Phone("P2", "One Plus Ace 3v", "Phone", 50, 299000));
            warehouse.Products.Add(new Tablet("P3", "Xiaomi pad 6", "Tablet", 30, 399000));
            warehouse.Products.Add(new Keyboard("P4", "Cidoo", "Keyboard", 100, 890000));
            warehouse.Products.Add(new Headphone("P5", "Tanjim Zero", "Headphone", 70, 199000));
            warehouse.Products.Add(new Mouse("P6", "G304", "Mouse", 80, 490000));
            warehouse.Products.Add(new Phone("P7", "Iphone 16 Pro Plus", "Phone", 50, 390000));
            warehouse.Products.Add(new Tablet("P8", "Ipad Gen 6", "Tablet", 60, 149000));
            warehouse.Products.Add(new Keyboard("P9", "Logitech K102", "Keyboard", 120, 690000));
            warehouse.Products.Add(new Headphone("P10", "Moondrop Jiu", "Headphone", 40, 299000));

            warehouse.Employees.Add(new Employee("E1", "Tommy James", "Employee", "U1", "P1"));
            warehouse.Employees.Add(new Employee("E2", "Ethan Nick", "Employee", "U2", "P2"));
            warehouse.Employees.Add(new Employee("E3", "Sofia Briam", "Employee", "U3", "P3"));
            warehouse.Employees.Add(new Employee("E4", "Liam Philip", "Employee", "U4", "P4"));
            warehouse.Employees.Add(new Employee("E5", "Micas Sa", "Employee", "U5", "P5"));
            warehouse.Employees.Add(new Employee("E6", "Luke Hamilton", "Employee", "U6", "P6"));
            warehouse.Employees.Add(new Employee("E7", "Mason Green", "Employee", "U7", "P7"));
            warehouse.Employees.Add(new Employee("E8", "Ohio Loto", "Employee", "U8", "P8"));
            warehouse.Employees.Add(new Employee("E9", "Mia James", "Employee", "U9", "P9"));
            warehouse.Employees.Add(new Employee("E10", "Mi chot So", "Employee", "U10", "P10"));

            warehouse.Inventory.Add(new Inventory(new Dictionary<String, int>() {
                    { "P1", 10 },
                    { "P2", 15 },
                    { "P3", 20 },
                    { "P4", 10 },
                    { "P5", 30 },
                    { "P6", 20 },
                    { "P7", 12 },
                    { "P8", 8 },
                    { "P9", 22 },
                    { "P10", 25 }
            }));

            // Supplier
            List<Supplier> suppliers = new List<Supplier>()
            {
                new Supplier("S1", "Cellphones", "0912345671",
                new List<Product>()
                {
                    new Phone("P1", "Iphone 16 Pro", "Phone", 100, 199000),
                    new Phone("P2", "One Plus Ace 3v", "Phone", 100, 299000),
                    new Phone("P7", "Iphone 16 Pro Plus", "Phone", 100, 390000),
                    new Keyboard("P4", "Cidoo", "Keyboard", 100, 890000),
                    new Mouse("P6", "G304", "Mouse", 100, 490000),
                    new Tablet("P8", "Ipad Gen 6", "Tablet", 100, 149000),
                    new Keyboard("P9", "Logitech K102", "Keyboard", 100, 690000)
                }
                ),

                new Supplier("S2", "Xiaomi", "0987655555",
                new List<Product>()
                {
                    new Tablet("P3", "Xiaomi pad 6", "Tablet", 100, 399000)
                }
                ),

                new Supplier("S3", "XuanVuAudio", "1230098006",
                new List<Product>()
                {
                    new Headphone("P5", "Tanjim Zero", "Headphone", 100, 199000),
                    new Headphone("P10", "Moondrop Jiu", "Headphone", 100, 299000)
                })
            };

            // PurchaseOrder
            List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>()
            {
                new PurchaseOrder("PD1",suppliers[0],"Pending",new List<Product>()
                {
                     new Phone("P1", "Iphone 16 Pro", "Phone", 40, 199000),
                     new Tablet("P8", "Ipad Gen 6", "Tablet", 5, 149000),
                     new Keyboard("P9", "Logitech K102", "Keyboard", 100, 690000)
                })
                ,
                new PurchaseOrder("PD2",suppliers[1],"Pending",new List<Product>()
                {
                    new Tablet("P3", "Xiaomi pad 6", "Tablet", 10, 399000)
                })
                ,
                new PurchaseOrder("PD3",suppliers[2],"Pending",new List<Product>()
                {
                    new Headphone("P5", "Tanjim Zero", "Headphone", 10, 199000),
                    new Headphone("P10", "Moondrop Jiu", "Headphone", 5, 299000)
                })
            };

            // ReturnOrder
            List<ReturnOrder> returnOrders = new List<ReturnOrder>()
            {
                new ReturnOrder("R1",
                new Phone("P1", "Iphone 16 Pro", "Phone", 3, 199000),"Broken During Transportation", new DateTime(2024,10,23),"Done"
                    ),
                new ReturnOrder("R2",
                new Headphone("P5", "Tanjim Zero", "Headphone", 2, 199000),"Broken During Transportation", new DateTime(2024,10,24),"Done"
                    ),
                new ReturnOrder("R3",
                new Keyboard("P4", "Cidoo", "Keyboard", 5, 890000),"Fix", new DateTime(2024,10,25),"Done"
                )
            };

            List<Customer> customers = new List<Customer>()
            {
                new Customer("C1","Viet","00000012345"),
                new Customer("C2","Huy","00234981233"),
                new Customer("C3","Cuong","0334901239")
            };


            // OrderManager
            OrderManager orderManager = new OrderManager(purchaseOrders);


            // SaleInvoice
            List<SaleInvoice> salesInvoices = new List<SaleInvoice>()
            {
                new SaleInvoice("SI01",customers[0],new List<Product>
                {
                     new Phone("P1", "Iphone 16 Pro", "Phone", 1, 199000)
                }
                ,199000,"Done")
            };


            // Report
            Report report = new Report(salesInvoices);


            /*
                Warehouse
                PurchaseOrder
                OrderManager
                Supplier
                SaleInvoice
                Report
                Transaction
             */

            Login login = new Login(warehouse);

            Application.Run(login);
        }
    }
}
