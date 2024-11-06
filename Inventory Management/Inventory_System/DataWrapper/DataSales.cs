using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inventory_Management.Inventory_System
{
    public class DataSales
    {
        public List<SalesInvoice> SalesInvoices { get; set; }
        public Report Report { get; set; }
        public DataSales(List<SalesInvoice> salesInvoices, Report report)
        {
            SalesInvoices = salesInvoices;
            Report = report;
        }
        public DataSales()
        {

        }
    }
}
