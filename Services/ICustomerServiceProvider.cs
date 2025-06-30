using HM_Bank_CoreApp.Models;

namespace HM_Bank_CoreApp.Services
{
    public interface ICustomerServiceProvider
    {
        double GetAccountBalance(string accountNumber);
        double Deposit(string accountNumber, double amount);
        double Withdraw(string accountNumber, double amount);
        void Transfer(string fromAcc, string toAcc, double amount);
        Account GetAccountDetails(string accountNumber);
    }
}
