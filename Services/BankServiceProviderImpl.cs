using HM_Bank_CoreApp.Models;
using HM_Bank_CoreApp.Repository;
using System;
using System.Collections.Generic;

namespace HM_Bank_CoreApp.Services
{
    public class BankServiceProviderImpl : CustomerServiceProviderImpl, IBankServiceProvider
    {
        public BankServiceProviderImpl(IBankRepository repository) : base(repository) { }

        public Account CreateAccount(Customer customer, string accType, double balance)
        {
            string accNumber = "HM" + DateTime.Now.ToString("yyMMddHHmmss"); // Unique format
            return _bankRepository.CreateAccount(customer, accNumber, accType, balance);
        }

        public List<Account> ListAccounts()
        {
            return _bankRepository.ListAccounts();
        }

        public void CalculateInterest()
        {
            _bankRepository.CalculateInterest();
        }
    }
}
