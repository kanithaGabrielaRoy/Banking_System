using HM_Bank_CoreApp.Models;
using HM_Bank_CoreApp.Repository;
using System;
using System.Collections.Generic;

namespace HM_Bank_CoreApp.Services
{
    public class CustomerServiceProviderImpl : ICustomerServiceProvider
    {
        protected readonly IBankRepository _bankRepository;

        public CustomerServiceProviderImpl(IBankRepository bankRepository)
        {
            _bankRepository = bankRepository;
        }

        public double Deposit(string accountNumber, double amount)
        {
            return _bankRepository.Deposit(accountNumber, amount);
        }

        public double GetAccountBalance(string accountNumber)
        {
            return _bankRepository.GetAccountBalance(accountNumber);
        }

        public Account GetAccountDetails(string accountNumber)
        {
            return _bankRepository.GetAccountDetails(accountNumber);
        }

        public double Withdraw(string accountNumber, double amount)
        {
            return _bankRepository.Withdraw(accountNumber, amount);
        }

        public bool Transfer(string fromAccount, string toAccount, double amount)
        {
            return _bankRepository.Transfer(fromAccount, toAccount, amount);
        }
    }
}
