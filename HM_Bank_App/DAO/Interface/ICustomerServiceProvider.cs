using HM_Bank_App.Entity;

namespace HM_Bank_App.DAO.Interface
{
    public interface ICustomerServiceProvider
    {
        double GetAccountBalance(long accNo);
        double Deposit(long accNo, double amount);
        double Withdraw(long accNo, double amount);
        void Transfer(long fromAcc, long toAcc, double amount);
        Account GetAccountDetails(long accNo);
    }
}