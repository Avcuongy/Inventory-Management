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
    public class Transaction : ISerializable
    {
        private string transactionId;
        private Product product;
        private DateTime transactionDate;
        public Transaction(string transactionId, Product product, DateTime transactionDate)
        {
            this.transactionId = transactionId;
            this.product = product;
            this.transactionDate = transactionDate;

        }
        public string TransactionId { get => transactionId; set => transactionId = value; }
        public Product Product { get => product; set => product = value; }
        public DateTime TransactionDate { get => transactionDate; set => transactionDate = value; }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TransactionDate", TransactionDate);
            info.AddValue("Product", Product, typeof(Product));
            info.AddValue("TransactionId", TransactionId);
        }
        public Transaction(SerializationInfo info, StreamingContext context)
        {
            TransactionId = info.GetString("TransactionDate");
            Product = (Product)info.GetValue("Product", typeof(Product));
            TransactionDate = (DateTime)info.GetValue("TransactionId", typeof(DateTime));
        }
        public Transaction() 
        { 
        }
    }
}
