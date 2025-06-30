using System;

namespace HM_Bank_CoreApp.Models
{
    public class CurrentAccount : Account
    {
        private const double OverdraftLimit = 10000;

        public override void Deposit(double amount)
        {
            if (amount > 0)
                Balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (amount <= (Balance + OverdraftLimit))
                Balance -= amount;
            else
                throw new Exception("Overdraft limit exceeded.");
        }

        public override double CalculateInterest()
        {
            return 0; // No interest for current account
        }
    }
}
