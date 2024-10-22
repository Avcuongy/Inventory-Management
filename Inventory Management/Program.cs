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
               

            Warehouse warehouse = new Warehouse();

            /*
                        warehouse.Products.Add(new Phone(1,"MX001","IPhone 13 pro",40,199000, new Bitmap("link")));
                        warehouse.Products.Add(new Phone(2, "MX002", "Smartphone", 50, 299000, new Bitmap("link")));
                        warehouse.Products.Add(new Tablet(3, "MX003", "Tablet Pro", 30, 399000, new Bitmap("link")));
                        warehouse.Products.Add(new Keyboard(4, "MX004", "Mechanical Keyboard", 100, 89000, new Bitmap("link")));
                        warehouse.Products.Add(new Headphone(5, "MX005", "Wireless Headphones", 70, 199000, new Bitmap("link")));
                        warehouse.Products.Add(new Mouse(6, "MX006", "Gaming Mouse", 80, 49000, new Bitmap("link")));
                        warehouse.Products.Add(new Phone(7, "MX007", "Basic Phone", 200, 39000, new Bitmap("link")));
                        warehouse.Products.Add(new Tablet(8, "MX008", "Budget Tablet", 60, 149000, new Bitmap("link")));
                        warehouse.Products.Add(new Keyboard(9, "MX009", "Wireless Keyboard", 120, 69000, new Bitmap("link")));
                        warehouse.Products.Add(new Headphone(10, "MX010", "Noise Cancelling Headphones", 40, 299000, new Bitmap("link")));*/

           

            warehouse.Employees.Add(new Employee(10, "A", "Employee", "U1", "P1"));
            warehouse.Employees.Add(new Employee(11, "B", "Employee", "U2", "P2"));
            warehouse.Employees.Add(new Employee(12, "C", "Employee", "U3", "P3"));
            warehouse.Employees.Add(new Employee(13, "D", "Employee", "U4", "P4"));
            warehouse.Employees.Add(new Employee(14, "E", "Employee", "U5", "P5"));
            warehouse.Employees.Add(new Employee(15, "F", "Employee", "U6", "P6"));
            warehouse.Employees.Add(new Employee(16, "G", "Employee", "U7", "P7"));
            warehouse.Employees.Add(new Employee(17, "H", "Employee", "U8", "P8"));
            warehouse.Employees.Add(new Employee(18, "I", "Employee", "U9", "P9"));
            warehouse.Employees.Add(new Employee(19, "J", "Employee", "U10", "P10"));
            warehouse.Employees.Add(new Employee(20, "K", "Employee", "U11", "P11"));


            warehouse.Inventory.Add(new Inventory(new Dictionary<int, int>() {
                    { 1, 10 },
                    { 2, 15 },
                    { 3, 20 },
                    { 4, 5 },
                    { 5, 25 },
                    { 6, 30 },
                    { 7, 12 },
                    { 8, 8 },
                    { 9, 20 },
                    { 10, 15 }
            }));


            Supplier supplier = new Supplier();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            OrderManager orderManager = new OrderManager();




            SaleInvoice saleInvoice = new SaleInvoice();
            Report report = new Report();
            Transaction transaction = new Transaction();

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
