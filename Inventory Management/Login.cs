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
    public partial class Login : Form
    {
        private Warehouse _warehouse;
        public Login(Warehouse warehouse)
        {
            InitializeComponent();
            _warehouse = warehouse;
        }
        private void Login_Button_Click(object sender, EventArgs e)
        {
            string username = Username.Text;
            string password = Password.Text;
            if (_warehouse.CheckUser(username, password))
            {
                MessageBox.Show("Login Successful!");
                this.Hide();
                Profile profile = new Profile();
                profile.Show();
            }    
        }
    }
}
