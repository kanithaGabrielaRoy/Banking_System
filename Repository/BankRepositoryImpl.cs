using System;
using System.Collections.Generic;
using HM_Bank_CoreApp.Models;

namespace HM_Bank_CoreApp.Repository
{
    public class BankRepositoryImpl : IBankRepository
    {
        private readonly List<Account> _accounts = new List<Account>();

        public Account CreateAccount(Customer customer, string accNo, string accType, double balance)
        {
            Account acc;
            switch (accType.ToLower())
            {
                case "savings":
                    acc = new SavingsAccount();
                    break;
                case "current":
                    acc = new CurrentAccount();
                    break;
                default:
                    throw new Exception("Invalid account type.");
            }

            acc.AccountNumber = accNo;
            acc.AccountType = accType;
            acc.Balance = balance;
            acc.Customer = customer;
            _accounts.Add(acc);

            return acc;
        }

        public List<Account> ListAccounts() => _accounts;

        public double Deposit(string accountNumber, double amount)
        {
            var acc = GetAccount(accountNumber);
            acc.Deposit(amount);
            return acc.Balance;
        }

        public double Withdraw(string accountNumber, double amount)
        {
            var acc = GetAccount(accountNumber);
            acc.Withdraw(amount);
            return acc.Balance;
        }

        public bool Transfer(string fromAccount, string toAccount, double amount)
        {
            var fromAcc = GetAccount(fromAccount);
            var toAcc = GetAccount(toAccount);
            fromAcc.Withdraw(amount);
            toAcc.Deposit(amount);
            return true;
        }

        public double GetAccountBalance(string accountNumber) =>
            GetAccount(accountNumber).Balance;

        public Account GetAccountDetails(string accountNumber) =>
            GetAccount(accountNumber);

        public void CalculateInterest()
        {
            foreach (var acc in _accounts)
            {
                if (acc is SavingsAccount)
                {
                    acc.Balance += acc.CalculateInterest();
                }
            }
        }

        private Account GetAccount(string accountNumber)
        {
            var acc = _accounts.Find(a => a.AccountNumber == accountNumber);
            if (acc == null)
                throw new Exception("Account not found.");
            return acc;
        }
    }
}
