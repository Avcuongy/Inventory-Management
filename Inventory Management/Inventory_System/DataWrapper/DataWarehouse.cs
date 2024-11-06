using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory_Management.Inventory_System
{
    public class DataWarehouse
    {
        public Warehouse Warehouse { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<Customer> Customers { get; set; }
        public DataWarehouse(Warehouse warehouse, List<Supplier> supplier, List<Customer> customer)
        {
            Warehouse = warehouse;
            Suppliers = supplier;
            Customers = customer;
        }
       public DataWarehouse()
        {

        }
    }
}
