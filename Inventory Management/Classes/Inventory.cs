using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory_Management
{
    public class Inventory : ISerializable
    {
        private Dictionary<string, int> productStock = new Dictionary<string, int>();
        public Dictionary<string, int> ProductStock { get => productStock; set => productStock = value; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ProductStock", ProductStock, typeof(Dictionary<string, int>));
        }
        public Inventory(SerializationInfo info, StreamingContext context)
        {
            ProductStock = (Dictionary<string, int>)info.GetValue("ProductStock", typeof(Dictionary<string, int>));
        }
        public Inventory()
        {

        }
        public Inventory(Dictionary<string, int> productStock)
        {
            this.productStock = productStock;
        }
        public void RemoveStock(string productId, int quantity)
        {
            foreach (string key in ProductStock.Keys)
            {
                if (key == productId)
                {
                    ProductStock[key] -= quantity;
                    break;
                }
            }
        }
        public void AddStock(string productId, int quantity)
        {

            foreach (string key in ProductStock.Keys)
            {
                if (key == productId)
                {
                    ProductStock[key] += quantity;
                    break;
                }
            }
        }
    }
}
