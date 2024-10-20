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

            Warehouse warehouse;


            string path = "Inventory_Management_data.dat";
            
            if (!File.Exists(path))
            {
                //Khởi tạo các giá trị
                warehouse = new Warehouse();
                
            }
            else
            {
                string fileContent = File.ReadAllText(path);
                warehouse = JsonSerializer.Deserialize<Warehouse>(fileContent);
            }

            Login login = new Login(warehouse);

            Application.Run(login);
        }
    }
}
