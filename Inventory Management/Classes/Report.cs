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
    public class Report : ISerializable
    {
        private List<SaleInvoice> invoices = new List<SaleInvoice>();
        public List<SaleInvoice> Invoices { get => invoices; set => invoices = value; }

        public Report() { }

        protected Report(SerializationInfo info, StreamingContext context)
        {
            Invoices = (List<SaleInvoice>)info.GetValue("Invoices", typeof(List<SaleInvoice>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Invoices", Invoices, typeof(List<SaleInvoice>));
        }
        public Report(List<SaleInvoice> invoices)
        {
            this.invoices = invoices;
        }
    }
}
