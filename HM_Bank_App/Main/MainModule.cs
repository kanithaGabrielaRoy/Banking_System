using HM_Bank_App.App;

namespace HM_Bank_App.Main
{
    class MainModule
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank(); // Instance of our test class
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n===== HMBank Menu =====");
                Console.WriteLine("1. Create Savings Account");
                Console.WriteLine("2. Create Current Account");
                Console.WriteLine("3. Create Zero Balance Account");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                try
                {
                    int choice = int.Parse(Console.ReadLine());
                    if (choice >= 1 && choice <= 3)
                    {
                        bank.Start(choice); // Trigger banking logic
                    }
                    else if (choice == 4)
                    {
                        running = false;
                        Console.WriteLine("Exiting. Thank you!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}