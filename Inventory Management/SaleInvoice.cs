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
    internal class SaleInvoice : ISerializable
    {
        private int invoiceId;
        private Customer customer;
        private List<Product> soldProducts;
        private double totalAmount;
        private string paymentStatus;

        public int InvoiceId { get => invoiceId; set => invoiceId = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public List<Product> SoldProducts { get => soldProducts; set => soldProducts = value; }
        public double TotalAmount { get => totalAmount; set => totalAmount = value; }
        public string PaymentStatus { get => paymentStatus; set => paymentStatus = value; }

        public SaleInvoice() { }

        public SaleInvoice(int id, Customer customer, List<Product> products)
        {
            this.invoiceId = id;
            this.customer = customer;
            this.soldProducts = products;
            this.totalAmount = CalculateTotal();
            this.paymentStatus = "Pending"; // Mặc định là chưa thanh toán
        }

        public SaleInvoice(SerializationInfo info, StreamingContext context)
        {
            InvoiceId = info.GetInt32("InvoiceId");
            Customer = (Customer)info.GetValue("Customer", typeof(Customer));
            SoldProducts = (List<Product>)info.GetValue("SoldProducts", typeof(List<Product>));
            TotalAmount = info.GetDouble("TotalAmount");
            PaymentStatus = info.GetString("PaymentStatus");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("InvoiceId", InvoiceId);
            info.AddValue("Customer", Customer, typeof(Customer));
            info.AddValue("SoldProducts", SoldProducts, typeof(List<Product>));
            info.AddValue("TotalAmount", TotalAmount);
            info.AddValue("PaymentStatus", PaymentStatus);
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var product in soldProducts)
            {
                total += product.Price;
            }
            return total;
        }
    }
}
