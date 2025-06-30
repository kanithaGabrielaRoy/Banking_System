using System;

namespace HM_Bank_CoreApp.Models
{
    public class ZeroBalanceAccount : Account
    {
        public override void Deposit(double amount)
        {
            if (amount > 0)
                Balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (amount <= Balance)
                Balance -= amount;
            else
                throw new Exception("Insufficient balance.");
        }

        public override double CalculateInterest()
        {
            return 0; // No interest for zero balance accounts
        }
    }
}
