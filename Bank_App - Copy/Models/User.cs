using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public BankAccount Account { get; set; }



        public User(string name, string username, string password, BankAccount account)
        {
            Name = name;
            Username = username;
            Password = password;
            Account = account;
        }
    }
}
