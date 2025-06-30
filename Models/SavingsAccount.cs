using System;

namespace HM_Bank_CoreApp.Models
{
    public class SavingsAccount : Account
    {
        private const double InterestRate = 0.045;

        public override void Deposit(double amount)
        {
            if (amount > 0)
                Balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (Balance - amount >= 500)
                Balance -= amount;
            else
                throw new Exception("Insufficient funds. Minimum balance of 500 must be maintained.");
        }

        public override double CalculateInterest()
        {
            return Balance * InterestRate;
        }
    }
}
