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
    internal class Report : ISerializable
    {
        private List<Transaction> transactions;
        private List<SaleInvoice> invoices;

        internal List<Transaction> Transactions { get => transactions; set => transactions = value; }
        internal List<SaleInvoice> Invoices { get => invoices; set => invoices = value; }

        public Report(List<Transaction> transactions, List<SaleInvoice> invoices)
        {
            this.Transactions = transactions;
            this.Invoices = invoices;
        }
        public Report() { }

        protected Report(SerializationInfo info, StreamingContext context)
        {
            Transactions = (List<Transaction>)info.GetValue("Transactions", typeof(List<Transaction>));
            Invoices = (List<SaleInvoice>)info.GetValue("Invoices", typeof(List<SaleInvoice>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Transactions", Transactions, typeof(List<Transaction>));
            info.AddValue("Invoices", Invoices, typeof(List<SaleInvoice>));
        }
        public Report(SerializationInfo info, StreamingContext context)
        {
            Transactions = (List<Transaction>)info.GetValue("Transactions", typeof(List<Transaction>));
            Invoices = (List<SaleInvoice>)info.GetValue("Invoices", typeof(List<SaleInvoice>));
        }
    }
}
