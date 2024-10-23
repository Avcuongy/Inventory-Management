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
    public partial class Profile : Form
    {
        private Warehouse _warehouse;
        private string _username;
        public Profile(string username,Warehouse warehouse)
        {
            InitializeComponent();
            _warehouse = warehouse;
            _username = username;
            ShowInfoInProfile();

        }     
        public void ShowInfoInProfile()
        {
            foreach (Employee employ in _warehouse.Employees)
            {
                if (employ.Username == _username)
                {
                    NameLabel.Text = employ.Name;
                    RoleText.Text = employ.Role;
                    EmployeeIDText.Text = employ.EmployeeId;
                    break;
                }
            }
        }
    }
}
