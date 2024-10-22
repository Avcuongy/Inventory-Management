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
    public class Customer : ISerializable
    {
        private string customerId;
        private string name;
        private string contactInfo;

        public string CustomerId { get => customerId; set => customerId = value; }
        public string Name { get => name; set => name = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }

        public Customer() { }

        public Customer(string customerId, string name, string contactInfo)
        {
            this.customerId = customerId;
            this.name = name;
            this.contactInfo = contactInfo;
        }

        public Customer(SerializationInfo info, StreamingContext context)
        {
            CustomerId = info.GetString("CustomerId");
            Name = info.GetString("Name");
            ContactInfo = info.GetString("ContactInfo");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("CustomerId", CustomerId);
            info.AddValue("Name", Name);
            info.AddValue("ContactInfo", ContactInfo);
        }
    }
}
