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
                    new Headphone("P10", "Moondrop Jiu", "Headphone", 1, 299),
                    new Phone("P11", "Samsung Galaxy S23", "Phone", 1, 999),
                    new Tablet("P12", "Microsoft Surface Pro", "Tablet", 1, 799),
                    new Mouse("P13", "Logitech MX Master 3", "Mouse", 1, 1200),
                    new Keyboard("P14", "Razer BlackWidow", "Keyboard", 1, 2200),
                    new Headphone("P15", "Sony WH-1000XM4", "Headphone", 1, 400),
                    new Phone("P16", "Google Pixel 8", "Phone", 1, 899),
                    new Tablet("P17", "Lenovo Tab P11", "Tablet", 1, 499),
                    new Mouse("P18", "Razer DeathAdder", "Mouse", 1, 800),
                    new Keyboard("P19", "Corsair K70", "Keyboard", 1, 2500),
                    new Headphone("P20", "Bose 700", "Headphone", 1, 450)
                };

                // Employees
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
                    new Employee("E10", "Mi chot So", "Employee", "U10", "P10"),
                    new Employee("E11", "Sarah Lee", "Employee", "U11", "P11"),
                    new Employee("E12", "David Kim", "Employee", "U12", "P12"),
                    new Employee("E13", "Chris Brown", "Employee", "U13", "P13"),
                    new Employee("E14", "Emma Watson", "Employee", "U14", "P14"),
                    new Employee("E15", "James Bond", "Employee", "U15", "P15"),
                    new Employee("E16", "Micheal Scott", "Employee", "U16", "P16"),
                    new Employee("E17", "Dwight Schrute", "Employee", "U17", "P17"),
                    new Employee("E18", "Pam Beesly", "Employee", "U18", "P18"),
                    new Employee("E19", "Jim Halpert", "Employee", "U19", "P19"),
                    new Employee("E20", "Angela Martin", "Employee", "U20", "P20")
                };

                // Inventory
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
                        { "P10", 34 },
                        { "P11", 10 },
                        { "P12", 15 },
                        { "P13", 5 },
                        { "P14", 8 },
                        { "P15", 12 },
                        { "P16", 7 },
                        { "P17", 18 },
                        { "P18", 20 },
                        { "P19", 6 },
                        { "P20", 9 }
                    }
                    )
                };

                // Categories
                warehouse.Categories = new List<Category>
                {
                    new Category("Phones", new List<Product>
                    {
                        new Phone("P1", "Iphone 16 Pro", "Phone", 1, 199),
                        new Phone("P2", "One Plus Ace 3v", "Phone", 1, 299),
                        new Phone("P7", "Iphone 16 Pro Plus", "Phone", 1, 390),
                        new Phone("P11", "Samsung Galaxy S23", "Phone", 1, 999),
                        new Phone("P16", "Google Pixel 8", "Phone", 1, 899)
                    }),
                    new Category("Tablets", new List<Product>
                    {
                        new Tablet("P3", "Xiaomi pad 6", "Tablet", 1, 399),
                        new Tablet("P8", "Ipad Gen 6", "Tablet", 1, 149),
                        new Tablet("P12", "Microsoft Surface Pro", "Tablet", 1, 799),
                        new Tablet("P17", "Lenovo Tab P11", "Tablet", 1, 499)
                    }),
                    new Category("Keyboards", new List<Product>
                    {
                        new Keyboard("P4", "Cidoo", "Keyboard", 1, 890),
                        new Keyboard("P9", "Logitech K102", "Keyboard", 1, 690),
                        new Keyboard("P14", "Razer BlackWidow", "Keyboard", 1, 2200),
                        new Keyboard("P19", "Corsair K70", "Keyboard", 1, 2500)
                    }),
                    new Category("Headphones", new List<Product>
                    {
                        new Headphone("P5", "Tanjim Zero", "Headphone", 1, 199),
                        new Headphone("P10", "Moondrop Jiu", "Headphone", 1, 299),
                        new Headphone("P15", "Sony WH-1000XM4", "Headphone", 1, 400),
                        new Headphone("P20", "Bose 700", "Headphone", 1, 450)
                    }),
                    new Category("Mice", new List<Product>
                    {
                        new Mouse("P6", "G304", "Mouse", 1, 490),
                        new Mouse("P13", "Logitech MX Master 3", "Mouse", 1, 1200),
                        new Mouse("P18", "Razer DeathAdder", "Mouse", 1, 800)
                    })
                };

                // Supplier
                suppliers = new List<Supplier>()
                {
                    new Supplier("S1", "Cellphones", "0912345671", new List<Product>
                    {
                        new Phone("P1", "Iphone 16 Pro", "Phone", 1, 199),
                        new Phone("P2", "One Plus Ace 3v", "Phone", 1, 299),
                        new Phone("P7", "Iphone 16 Pro Plus", "Phone", 1, 399),
                        new Keyboard("P4", "Cidoo", "Keyboard", 1, 890),
                        new Mouse("P6", "G304", "Mouse", 1, 490),
                        new Tablet("P8", "Ipad Gen 6", "Tablet", 1, 149),
                        new Keyboard("P9", "Logitech K102", "Keyboard", 1, 690),
                        new Phone("P11", "Samsung Galaxy S23", "Phone", 1, 999)
                    }),

                    new Supplier("S2", "Xiaomi", "0987655555", new List<Product>
                    {
                        new Tablet("P3", "Xiaomi pad 6", "Tablet", 1, 399),
                        new Tablet("P17", "Lenovo Tab P11", "Tablet", 1, 499)
                    }),

                    new Supplier("S3", "XuanVuAudio", "1230098006", new List<Product>
                    {
                        new Headphone("P5", "Tanjim Zero", "Headphone", 1, 199),
                        new Headphone("P10", "Moondrop Jiu", "Headphone", 1, 299)
                    }),

                    new Supplier("S4", "Samsung", "0981234567", new List<Product>
                    {
                        new Phone("P11", "Samsung Galaxy S23", "Phone", 1, 999),
                        new Phone("P16", "Google Pixel 8", "Phone", 1, 899),
                        new Headphone("P20", "Bose 700", "Headphone", 1, 450)
                    }),

                    new Supplier("S5", "Sony", "0987654321", new List<Product>
                    {
                        new Headphone("P15", "Sony WH-1000XM4", "Headphone", 1, 400)
                    }),

                    new Supplier("S6", "Microsoft", "1234567890", new List<Product>
                    {
                        new Tablet("P12", "Microsoft Surface Pro", "Tablet", 1, 799)
                    }),

                    new Supplier("S7", "Logitech", "9876543210", new List<Product>
                    {
                        new Mouse("P13", "Logitech MX Master 3", "Mouse", 1, 1200),
                        new Keyboard("P14", "Razer BlackWidow", "Keyboard", 1, 2200)
                    }),

                    new Supplier("S8", "Razer", "0123456789", new List<Product>
                    {
                        new Mouse("P18", "Razer DeathAdder", "Mouse", 1, 800),
                        new Keyboard("P19", "Corsair K70", "Keyboard", 1, 2500)
                    })
                };

                // PurchaseOrder
                purchaseOrders = new List<PurchaseOrder>()
                {
                    new PurchaseOrder("PD1", suppliers[0], "Pending", new List<Product>
                    {
                        new Phone("P1", "Iphone 16 Pro", "Phone", 40, 7960),
                        new Tablet("P8", "Ipad Gen 6", "Tablet", 5, 745),
                        new Keyboard("P9", "Logitech K102", "Keyboard", 100, 69000)
                    }),
                    new PurchaseOrder("PD2", suppliers[1], "Pending", new List<Product>
                    {
                        new Tablet("P3", "Xiaomi pad 6", "Tablet", 10, 3990)
                    }),
                    new PurchaseOrder("PD3", suppliers[2], "Pending", new List<Product>
                    {
                        new Headphone("P5", "Tanjim Zero", "Headphone", 10, 1990),
                        new Headphone("P10", "Moondrop Jiu", "Headphone", 5, 1495)
                    }),
                    new PurchaseOrder("PD4", suppliers[3], "Pending", new List<Product>
                    {
                        new Phone("P11", "Samsung Galaxy S23", "Phone", 20, 19980),
                        new Phone("P16", "Google Pixel 8", "Phone", 15, 13485)
                    }),
                    new PurchaseOrder("PD5", suppliers[4], "Pending", new List<Product>
                    {
                        new Headphone("P15", "Sony WH-1000XM4", "Headphone", 5, 2000)
                    }),
                    new PurchaseOrder("PD6", suppliers[5], "Pending", new List<Product>
                    {
                        new Tablet("P12", "Microsoft Surface Pro", "Tablet", 10, 7990)
                    }),
                    new PurchaseOrder("PD7", suppliers[6], "Pending", new List<Product>
                    {
                        new Mouse("P13", "Logitech MX Master 3", "Mouse", 50, 60000)
                    }),
                    new PurchaseOrder("PD8", suppliers[7], "Pending", new List<Product>
                    {
                        new Mouse("P18", "Razer DeathAdder", "Mouse", 30, 24000),
                        new Keyboard("P19", "Corsair K70", "Keyboard", 20, 50000)
                    })
                };

                // ReturnOrder
                returnOrders = new List<ReturnOrder>()
                {
                    new ReturnOrder("R1", new Phone("P1", "Iphone 16 Pro", "Phone", 3, 597), "Broken During Transportation", new DateTime(2024, 10, 23), "Done"),
                    new ReturnOrder("R2", new Headphone("P5", "Tanjim Zero", "Headphone", 2, 398), "Broken During Transportation", new DateTime(2024, 10, 24), "Done"),
                    new ReturnOrder("R3", new Keyboard("P4", "Cidoo", "Keyboard", 5, 4450), "Broken During Transportation", new DateTime(2024, 10, 25), "Done"),
                    new ReturnOrder("R4", new Phone("P11", "Samsung Galaxy S23", "Phone", 1, 999), "Defective", new DateTime(2024, 10, 26), "Pending"),
                    new ReturnOrder("R5", new Headphone("P15", "Sony WH-1000XM4", "Headphone", 1, 400), "Not as Described", new DateTime(2024, 10, 27), "Pending"),
                    new ReturnOrder("R6", new Tablet("P12", "Microsoft Surface Pro", "Tablet", 1, 799), "Damaged", new DateTime(2024, 10, 28), "Pending"),
                    new ReturnOrder("R7", new Mouse("P13", "Logitech MX Master 3", "Mouse", 1, 1200), "Wrong Item Sent", new DateTime(2024, 10, 29), "Pending"),
                    new ReturnOrder("R8", new Keyboard("P19", "Corsair K70", "Keyboard", 1, 2500), "Broken During Transportation", new DateTime(2024, 10, 30), "Pending")
                };

                // Customers
                customers = new List<Customer>()
                {
                    new Customer("C1", "Viet", "00000012345"),
                    new Customer("C2", "Huy", "00234981233"),
                    new Customer("C3", "Cuong", "0334901239"),
                    new Customer("C4", "Huong", "12345678901"),
                    new Customer("C5", "Binh", "23456789012"),
                    new Customer("C6", "Tam", "34567890123"),
                    new Customer("C7", "Khanh", "45678901234"),
                    new Customer("C8", "An", "56789012345")
                };

                // orderManager
                orderManager = new OrderManager(purchaseOrders);

                // SalesInvoice
                salesInvoices = new List<SalesInvoice>()
                {
                    new SalesInvoice("SI01", customers[0], new List<Product>
                    {
                        new Phone("P1", "Iphone 16 Pro", "Phone", 4, 199)
                    }, "Paid"),
                    new SalesInvoice("SI02", customers[1], new List<Product>
                    {
                        new Tablet("P8", "Ipad Gen 6", "Tablet", 2, 298)
                    }, "Unpaid"),
                    new SalesInvoice("SI03", customers[2], new List<Product>
                    {
                        new Phone("P11", "Samsung Galaxy S23", "Phone", 2, 999)
                    }, "Paid"),
                    new SalesInvoice("SI04", customers[3], new List<Product>
                    {
                        new Headphone("P15", "Sony WH-1000XM4", "Headphone", 1, 400)
                    }, "Paid"),
                    new SalesInvoice("SI05", customers[4], new List<Product>
                    {
                        new Tablet("P12", "Microsoft Surface Pro", "Tablet", 1, 799)
                    }, "Paid"),
                    new SalesInvoice("SI06", customers[5], new List<Product>
                    {
                        new Mouse("P13", "Logitech MX Master 3", "Mouse", 5, 1200)
                    }, "Paid"),
                    new SalesInvoice("SI07", customers[6], new List<Product>
                    {
                        new Mouse("P18", "Razer DeathAdder", "Mouse", 1, 800)
                    }, "Paid"),
                    new SalesInvoice("SI08", customers[7], new List<Product>
                    {
                        new Keyboard("P19", "Corsair K70", "Keyboard", 2, 2500)
                    }, "Unpaid")
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
            //create Login
            Application.Run(login);
        }
    }
}
