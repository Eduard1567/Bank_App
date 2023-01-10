using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_App.Models
{
    public class BankAccount
    {
        public string IdNumber { get; set; }
        public double Balance { get; set; }


        public BankAccount(string idNumber)
        {
            this.IdNumber = idNumber;
            this.Balance = 0;
        }



        public void Deposit(double amount)
        {
            Balance += amount;

        }

        public void Withdraw()
        {
            // ...
        }

        public void Transfer(BankAccount reciever)
        {
            // ...
        }
    }

}
