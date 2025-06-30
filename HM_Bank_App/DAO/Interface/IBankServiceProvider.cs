using HM_Bank_App.Entity;

namespace HM_Bank_App.DAO.Interface
{
    public interface IBankServiceProvider
    {
        Account CreateAccount(Customer customer, string accType, double balance);
        List<Account> ListAccounts();
        double CalculateInterest(Account account);
    }
}
