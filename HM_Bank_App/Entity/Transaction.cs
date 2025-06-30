namespace HM_Bank_App.Entity
{
    public class Transaction
    {
        public long TransactionId { get; set; }
        public long AccountNumber { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } // Withdraw, Deposit, Transfer
        public double TransactionAmount { get; set; }
    }
}