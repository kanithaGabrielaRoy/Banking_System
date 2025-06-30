using System;

namespace HM_Bank_CoreApp.Models
{
    public abstract class Account
    {
        public string AccountNumber { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public Customer Customer { get; set; }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract double CalculateInterest();
    }
}
