using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management
{
    public class Mouse:Product
    {
        public Mouse(int productId, string name, string category, int quantity, double price, Bitmap productImage) : base()
        {
            ProductId = productId;
            Name = name;
            Category = category;
            Quantity = quantity;
            Price = price;
            ProductImage = productImage;
        }
    }
}
