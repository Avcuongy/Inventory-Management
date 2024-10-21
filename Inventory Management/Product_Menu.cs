using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory_Management
{
    public partial class Product_Menu : Form
    {
        public Product_Menu()
        {
            InitializeComponent();
        }
        private void Click_button_Add_Product(object sender, EventArgs e)
        {
            Product_Add product_Add = new Product_Add();
            product_Add.Show();
        }

        private void Click_Button_Update_Product(object sender, EventArgs e)
        {
            Product_Update product_Update = new Product_Update();
            product_Update.Show();
        }
    }
}
