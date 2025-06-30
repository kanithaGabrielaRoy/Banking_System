using HM_Bank_App.Entity;

namespace HM_Bank_App.Repository
{
    public interface IBankRepository
    {
        Account CreateAccount(Customer customer, string accNo, string accType, double balance);
        List<Account> ListAccounts();
        double Deposit(string accountNumber, double amount);
        double Withdraw(string accountNumber, double amount);
        bool Transfer(string fromAccount, string toAccount, double amount);
        double GetAccountBalance(string accountNumber);
        Account GetAccountDetails(string accountNumber);
        void CalculateInterest();
    }
}
