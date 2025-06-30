using System.Collections.Generic;
using HM_Bank_CoreApp.Models;

namespace HM_Bank_CoreApp.Services
{
    public interface IBankServiceProvider : ICustomerServiceProvider
    {
        void CreateAccount(Customer customer, string accType, double initialBalance);
        List<Account> ListAccounts();
        void CalculateInterest();
    }
}
