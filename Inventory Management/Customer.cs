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
    internal class Customer : ISerializable
    {
        private int customerId;
        private string name;
        private string contactInfo;
        
        public int CustomerId { get => customerId; set => customerId = value; }
        public string Name { get => name; set => name = value; }
        public string ContactInfo { get => contactInfo; set => contactInfo = value; }

        public Customer() { }

        public Customer(int customerId, string name, string contactInfo)
        {
            this.customerId = customerId;
            this.name = name;
            this.contactInfo = contactInfo;
        }

        protected Customer(SerializationInfo info, StreamingContext context)
        {
            customerId = info.GetInt32("customerId");
            name = info.GetString("name");
            contactInfo = info.GetString("contactInfo");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("customerId", customerId);
            info.AddValue("name", name);
            info.AddValue("contactInfo", contactInfo);
        }
}
