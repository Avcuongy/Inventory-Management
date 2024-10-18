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
    internal class Transaction : ISerializable
    {
        private int transactionId;
        private Product product;
        private int quantity;
        private DateTime transactionDate;
        private Employee handledBy;
        public Transaction(int transactionId, Product product, int quantity, DateTime transactionDate, Employee handledBy)
        {
            this.transactionId = transactionId;
            this.product = product;
            this.quantity = quantity;
            this.transactionDate = transactionDate;
            this.handledBy = handledBy;
        }
        public int TransactionId { get => transactionId; set => transactionId = value; }
        public Product Product { get => product; set => product = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public DateTime TransactionDate { get => transactionDate; set => transactionDate = value; }
        internal Employee HandledBy { get => handledBy; set => handledBy = value; }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TransactionDate", TransactionDate);
            info.AddValue("HandledBy", HandledBy);
            info.AddValue("Product", Product, typeof(Product));
            info.AddValue("Quantity", Quantity);
            info.AddValue("TransactionId", TransactionId);
        }
        public Transaction(SerializationInfo info, StreamingContext context)
        {
            TransactionId = info.GetInt32("TransactionDate");
            HandledBy = (Employee)info.GetValue("HandledBy", typeof(Employee));
            Product = (Product)info.GetValue("Product", typeof(Product));
            Quantity = info.GetInt32("Quantity");
            TransactionDate = (DateTime)info.GetValue("TransactionId", typeof(DateTime));
        }
        public Transaction() 
        { 
        }
    }
}
