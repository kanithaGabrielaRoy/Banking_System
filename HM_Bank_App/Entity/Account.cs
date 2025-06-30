using HM_Bank_App.ExceptionLayer;


namespace HM_Bank_App.Entity
{
    public abstract class Account
    {
        private static long lastAccNo = 1000;

        public long AccountNumber { get; private set; }
        public string AccountType { get; protected set; }
        public double Balance { get; protected set; }
        public Customer AccountHolder { get; set; }

        public Account(string accountType, double balance, Customer customer)
        {
            AccountNumber = ++lastAccNo;
            AccountType = accountType;
            Balance = balance;
            AccountHolder = customer;
        }

        public abstract void Deposit(double amount);
        public abstract void Withdraw(double amount);
        public abstract double CalculateInterest();
    }

    public class SavingsAccount : Account
    {
        private const double InterestRate = 4.5;

        public SavingsAccount(double balance, Customer customer)
            : base("Savings", balance >= 500 ? balance : throw new ArgumentException("Minimum balance for savings is 500"), customer)
        {
        }

        public override void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new InsufficientFundException("Insufficient funds in savings account.");
            Balance -= amount;
        }

        public override double CalculateInterest()
        {
            return Balance * InterestRate / 100;
        }
    }

    public class CurrentAccount : Account
    {
        private const double OverdraftLimit = 1000;

        public CurrentAccount(double balance, Customer customer)
            : base("Current", balance, customer)
        {
        }

        public override void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (amount > Balance + OverdraftLimit)
                throw new OverDraftLimitExceededException("Exceeded overdraft limit.");
            Balance -= amount;
        }

        public override double CalculateInterest()
        {
            return 0; // No interest on current account
        }
    }

    public class ZeroBalanceAccount : Account
    {
        public ZeroBalanceAccount(Customer customer)
            : base("ZeroBalance", 0, customer)
        {
        }

        public override void Deposit(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            Balance += amount;
        }

        public override void Withdraw(double amount)
        {
            if (amount > Balance)
                throw new InsufficientFundException("Insufficient funds in zero balance account.");
            Balance -= amount;
        }

        public override double CalculateInterest()
        {
            return 0; // No interest on zero balance accounts
        }
    }
}