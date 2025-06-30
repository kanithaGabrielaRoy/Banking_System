using System;

namespace HM_Bank_CoreApp.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string AccountNumber { get; set; }
        public string TransactionType { get; set; } // Deposit, Withdrawal, Transfer
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}

