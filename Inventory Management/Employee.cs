using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Inventory_Management
{
    internal class Employee : ISerializable
    {
        private int employeeId;
        private string name;
        private string role;
        private string username;
        private string password;
        public int EmployeeId { get => employeeId; set => employeeId = value; }
        public string Name { get => name; set => name = value; }
        public string Role { get => role; set => role = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public Employee() { }

        protected Employee(SerializationInfo info, StreamingContext context)
        {
            EmployeeId = info.GetInt32("EmployeeId");
            Name = info.GetString("Name");
            Role = info.GetString("Role");
            Username = info.GetString("Username");
            Password = info.GetString("Password");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("EmployeeId", EmployeeId);
            info.AddValue("Name", Name);
            info.AddValue("Role", Role);
            info.AddValue("Username", Username);
            info.AddValue("Password", Password);
        }
        public bool Validate(string inputUsername, string inputPassword)
        {
            return Username == inputUsername && Password == inputPassword;
        } 
    }
}