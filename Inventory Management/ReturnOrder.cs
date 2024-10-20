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
    public class ReturnOrder : ISerializable
    {
        private int returnOrderId;
        private Product product;
        private int quantity;
        private string reason;
        private DateTime returnDate;
        private string status;

        public int ReturnOrderId { get => returnOrderId; set => returnOrderId = value; }
        public Product Product { get => product; set => product = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Reason { get => reason; set => reason = value; }
        public DateTime ReturnDate { get => returnDate; set => returnDate = value; }
        public string Status { get => status; set => status = value; }

        public ReturnOrder() { }

        
        public ReturnOrder(SerializationInfo info, StreamingContext context)
        {
            ReturnOrderId = info.GetInt32("ReturnOrderId");
            Product = (Product)info.GetValue("Product", typeof(Product));
            Quantity = info.GetInt32("Quantity");
            Reason = info.GetString("Reason");
            ReturnDate = info.GetDateTime("ReturnDate");
            Status = info.GetString("Status");
        }
        public ReturnOrder(int returnOrderId, Product product, int quantity, string reason, DateTime returnDate, string status)
        {
            this.returnOrderId = returnOrderId;
            this.product = product;
            this.quantity = quantity;
            this.reason = reason;
            this.returnDate = returnDate;
            this.status = status;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ReturnOrderId", ReturnOrderId);
            info.AddValue("Product", Product, typeof(Product));
            info.AddValue("Quantity", Quantity);
            info.AddValue("Reason", Reason);
            info.AddValue("ReturnDate", ReturnDate);
            info.AddValue("Status", Status);
        }
    }
}
