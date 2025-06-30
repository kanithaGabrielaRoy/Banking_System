using HM_Bank_App.ExceptionLayer;
using HM_Bank_App.Entity;

namespace HM_Bank_App.App
{
    public class Bank
    {
        public void Start(int accountType)
        {
            Account account = null;

            // Sample customer (replace with user input if needed)
            Customer customer = new Customer(1, "Alice", "Smith", "alice@example.com", "9876543210", "Pune");

            try
            {
                // Account creation logic
                switch (accountType)
                {
                    case 1:
                        Console.Write("Enter initial balance (min ₹500 for savings): ");
                        double sbal = double.Parse(Console.ReadLine());
                        account = new SavingsAccount(sbal, customer);
                        break;

                    case 2:
                        Console.Write("Enter initial balance for current account: ");
                        double cbal = double.Parse(Console.ReadLine());
                        account = new CurrentAccount(cbal, customer);
                        break;

                    case 3:
                        account = new ZeroBalanceAccount(customer);
                        Console.WriteLine("Zero Balance Account created with ₹0 balance.");
                        break;

                    default:
                        Console.WriteLine("Invalid account type selected.");
                        return;
                }

                Console.WriteLine($"\nAccount Created: {account.AccountType} - {account.AccountNumber}");
                Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Current Balance: ₹{account.Balance}");

                // Deposit
                Console.Write("\nEnter deposit amount: ");
                double deposit = double.Parse(Console.ReadLine());
                account.Deposit(deposit);
                Console.WriteLine($"Balance after deposit: ₹{account.Balance}");

                // Withdraw
                Console.Write("Enter withdrawal amount: ");
                double withdraw = double.Parse(Console.ReadLine());
                account.Withdraw(withdraw);
                Console.WriteLine($"Balance after withdrawal: ₹{account.Balance}");

                // Interest
                double interest = account.CalculateInterest();
                Console.WriteLine($"Interest (if applicable): ₹{interest}");
            }
            catch (InsufficientFundException ex)
            {
                Console.WriteLine("InsufficientFundException: " + ex.Message);
            }
            catch (OverDraftLimitExceededException ex)
            {
                Console.WriteLine("OverDraftLimitExceededException: " + ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Invalid input: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}