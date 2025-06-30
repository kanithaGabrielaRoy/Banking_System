using System;
using HM_Bank_CoreApp.Models;
using HM_Bank_CoreApp.Repository;
using HM_Bank_CoreApp.Services;

namespace HM_Bank_CoreApp.App
{
    public class BankApp
    {
        public static void Start()
        {
            IBankRepository repository = new BankRepositoryImpl();
            IBankServiceProvider bankService = new BankServiceProviderImpl(repository);

            while (true)
            {
                Console.WriteLine("\n--- HM Bank Menu ---");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Get Balance");
                Console.WriteLine("5. Transfer");
                Console.WriteLine("6. List Accounts");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("Enter Customer First Name: ");
                            string fname = Console.ReadLine();
                            Console.Write("Enter Last Name: ");
                            string lname = Console.ReadLine();
                            Console.Write("Enter Email: ");
                            string email = Console.ReadLine();
                            Console.Write("Enter Phone: ");
                            string phone = Console.ReadLine();
                            Console.Write("Enter Address: ");
                            string addr = Console.ReadLine();

                            Customer customer = new Customer
                            {
                                FirstName = fname,
                                LastName = lname,
                                Email = email,
                                PhoneNumber = phone,
                                Address = addr
                            };

                            Console.Write("Enter Account Type (Savings/Current): ");
                            string type = Console.ReadLine();
                            Console.Write("Enter Initial Balance: ");
                            double bal = Convert.ToDouble(Console.ReadLine());

                            Account newAcc = bankService.CreateAccount(customer, type, bal);
                            Console.WriteLine($"Account created: {newAcc.AccountNumber}");
                            break;

                        case 2:
                            Console.Write("Enter Account Number: ");
                            string accNo = Console.ReadLine();
                            Console.Write("Enter Deposit Amount: ");
                            double amount = Convert.ToDouble(Console.ReadLine());
                            double newBal = bankService.Deposit(accNo, amount);
                            Console.WriteLine($"New Balance: {newBal}");
                            break;

                        case 3:
                            Console.Write("Enter Account Number: ");
                            string accW = Console.ReadLine();
                            Console.Write("Enter Withdraw Amount: ");
                            double amtW = Convert.ToDouble(Console.ReadLine());
                            double balW = bankService.Withdraw(accW, amtW);
                            Console.WriteLine($"New Balance: {balW}");
                            break;

                        case 4:
                            Console.Write("Enter Account Number: ");
                            string accB = Console.ReadLine();
                            double balB = bankService.GetAccountBalance(accB);
                            Console.WriteLine($"Balance: {balB}");
                            break;

                        case 5:
                            Console.Write("From Account: ");
                            string from = Console.ReadLine();
                            Console.Write("To Account: ");
                            string to = Console.ReadLine();
                            Console.Write("Amount: ");
                            double amtT = Convert.ToDouble(Console.ReadLine());
                            bool success = bankService.Transfer(from, to, amtT);
                            Console.WriteLine(success ? "Transfer Successful" : "Transfer Failed");
                            break;

                        case 6:
                            foreach (var acc in bankService.ListAccounts())
                            {
                                Console.WriteLine($"AccNo: {acc.AccountNumber}, Name: {acc.Customer.FirstName}, Bal: {acc.Balance}");
                            }
                            break;

                        case 7:
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}
