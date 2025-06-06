﻿using System;
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
    public class SalesInvoice : ISerializable
    {
        private string invoiceId;
        private Customer customer;
        private List<Product> soldProducts = new List<Product>();
        private string paymentStatus;

        public string InvoiceId { get => invoiceId; set => invoiceId = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public List<Product> SoldProducts { get => soldProducts; set => soldProducts = value; }
        public string PaymentStatus { get => paymentStatus; set => paymentStatus = value; }
        public SalesInvoice() { }
        public SalesInvoice(SerializationInfo info, StreamingContext context)
        {
            InvoiceId = info.GetString("InvoiceId");
            Customer = (Customer)info.GetValue("Customer", typeof(Customer));
            SoldProducts = (List<Product>)info.GetValue("SoldProducts", typeof(List<Product>));
            PaymentStatus = info.GetString("PaymentStatus");
        }
        public SalesInvoice(string invoiceId, Customer customer, List<Product> soldProducts, string paymentStatus)
        {
            this.invoiceId = invoiceId;
            this.customer = customer;
            this.soldProducts = soldProducts;
            this.paymentStatus = paymentStatus;
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InvoiceId", InvoiceId);
            info.AddValue("Customer", Customer, typeof(Customer));
            info.AddValue("SoldProducts", SoldProducts, typeof(List<Product>));
            info.AddValue("PaymentStatus", PaymentStatus);
        }
    }
}
