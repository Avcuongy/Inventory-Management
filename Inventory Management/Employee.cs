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
    
        public Employee(int employeeId, string name, string role, string username, string password)
        {
            EmployeeId = employeeId;
            Name = name;
            Role = role;
            Username = username;
            Password = password;
        }

        public Employee() { }

        protected Employee(SerializationInfo info, StreamingContext context)
        {
            employeeId = info.GetInt32("employeeId");
            name = info.GetString("name");
            role = info.GetString("role");
            username = info.GetString("username");
            password = info.GetString("password");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("employeeId", employeeId);
            info.AddValue("name", name);
            info.AddValue("role", role);
            info.AddValue("username", username);
        }

        public bool Validate(string inputUsername, string inputPassword)
        {
            return username == inputUsername && password == inputPassword;
        } Employee() 
        { }
    }
}