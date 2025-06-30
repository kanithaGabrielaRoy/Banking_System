using HM_Bank_App.DAO.Interface;
using HM_Bank_App.Entity;            // Interfaces like IBankServiceProvider

namespace HM_Bank_App.DAO.Implementation
{
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        private readonly HashSet<Account> accountSet = new HashSet<Account>();
        private readonly Dictionary<long, Account> accountMap = new Dictionary<long, Account>();

        public string BranchName { get; set; } = "Main Branch";
        public string BranchAddress { get; set; } = "City Center";

        // Implements IBankServiceProvider.CreateAccount
        public Account CreateAccount(Customer customer, string accType, double balance)
        {
            Account acc = accType.ToLower() switch
            {
                "savings" => new SavingsAccount(balance, customer),
                "current" => new CurrentAccount(balance, customer),
                "zerobalance" => new ZeroBalanceAccount(customer),
                _ => throw new ArgumentException("Invalid account type.")
            };

            if (accountSet.Add(acc))
            {
                accountMap[acc.AccountNumber] = acc;
                return acc;
            }
            else
            {
                throw new Exception("Account already exists.");
            }
        }

        // Implements IBankServiceProvider.ListAccounts
        public List<Account> ListAccounts()
        {
            var sorted = accountSet.ToList();
            sorted.Sort();
            return sorted;
        }

        // Implements IBankServiceProvider.CalculateInterest
        public double CalculateInterest(Account account)
        {
            return account.AccountType.Equals("Savings", StringComparison.OrdinalIgnoreCase)
                ? account.Balance * 0.045
                : 0;
        }

        // Overrides base method (must be marked virtual in base class)
        protected virtual Account FindAccount(long accNo)
        {
            throw new NotImplementedException("Must be overridden in derived class.");
        }
    }
}