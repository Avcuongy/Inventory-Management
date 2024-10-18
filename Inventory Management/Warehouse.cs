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
    internal class Warehouse:ISerializable
    {
        private List<Product> products;
        private List<Employee> employees;
        Inventory inventory;
        public Warehouse(List<Product> products, List<Employee> employees, Inventory inventory)
        {
            this.products = products;
            this.employees = employees;
            this.inventory = inventory;
        }
        internal List<Product> Products { get => products; set => products = value; }
        internal List<Employee> Employees { get => employees; set => employees = value; }
        internal Inventory Inventory { get => inventory; set => inventory = value; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Products", Products, typeof(List<Product>));
            info.AddValue("Employees", Employees, typeof(List<Employee>));
            info.AddValue("Inventory", Inventory, typeof(Inventory));
        }
        public Warehouse(SerializationInfo info, StreamingContext context)
        {
            Products = (List<Product>)info.GetValue("Products",typeof(List<Product>));
            Employees = (List<Employee>)info.GetValue("Employees", typeof(List<Employee>));
            Inventory = (Inventory)info.GetValue("Inventory", typeof(Inventory));
        }
        public Warehouse()
        { }
        public void AddProduct(Product product)
        {
            products.Add(product);
        }
        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

    }
}
