using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management
{
    public class Headphone : Product
    {
        public Headphone(string productId, string name, string category, int quantity, double price) : base(productId, name, category, quantity, price)
        {
        }
    }
}
