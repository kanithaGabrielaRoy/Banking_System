using HM_Bank_App.DAO.Interface;
using HM_Bank_App.ExceptionLayer;
using HM_Bank_App.Entity;

namespace HM_Bank_App.DAO.Implementation
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        protected List<Account> accounts = new List<Account>();

        protected Account FindAccount(long accNo)
        {
            var acc = accounts.Find(a => a.AccountNumber == accNo);
            if (acc == null)
                throw new InvalidAccountException($"Account {accNo} not found.");
            return acc;
        }

        public double GetAccountBalance(long accNo)
        {
            return FindAccount(accNo).Balance;
        }

        public double Deposit(long accNo, double amount)
        {
            var acc = FindAccount(accNo);
            acc.Deposit(amount);
            return acc.Balance;
        }

        public double Withdraw(long accNo, double amount)
        {
            var acc = FindAccount(accNo);
            acc.Withdraw(amount);
            return acc.Balance;
        }

        public void Transfer(long fromAcc, long toAcc, double amount)
        {
            var from = FindAccount(fromAcc);
            var to = FindAccount(toAcc);
            from.Withdraw(amount);
            to.Deposit(amount);
        }

        public Account GetAccountDetails(long accNo)
        {
            return FindAccount(accNo);
        }
    }
}