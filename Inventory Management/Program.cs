using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

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

<<<<<<< HEAD
           
      
            Warehouse_Menu warehouse = new Warehouse_Menu();
=======
            Warehouse warehouse = new Warehouse();
            Inventory inventory = new Inventory();
            Category category = new Category();
            PurchaseOrder purchaseOrder = new PurchaseOrder();
            OrderManager orderManager = new OrderManager();
            Supplier supplier = new Supplier();
            SaleInvoice saleInvoice = new SaleInvoice();
            Report report = new Report();
            Transaction transaction = new Transaction();

            /*
                Warehouse
                Inventory
                Category
                PurchaseOrder
                OrderManager
                Supplier
                SaleInvoice
                Report
                Transaction
             */



>>>>>>> Cuong



            Login login = new Login(warehouse);

            Application.Run(login);
        }
    }
}
