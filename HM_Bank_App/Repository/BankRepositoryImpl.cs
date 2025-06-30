using HM_Bank_App.Entity;
using HM_Bank_App.Util;
using System.Data.SqlClient; // Required for ADO.NET commands

namespace HM_Bank_App.Repository
{
    public class BankRepositoryImpl
    {
        // Inserts a customer and associated account into the database
        public void CreateAccount(Customer cust, long accNo, string accType, float balance)
        {
            using var conn = DBConnUtil.GetConnection();

            string insertCustomer = @"INSERT INTO Customers (customer_id, first_name, last_name, email, phone_number, address)
                                      VALUES (@id, @fn, @ln, @em, @ph, @addr)";
            using var cmd1 = new SqlCommand(insertCustomer, conn);
            cmd1.Parameters.AddWithValue("@id", cust.CustomerId);
            cmd1.Parameters.AddWithValue("@fn", cust.FirstName);
            cmd1.Parameters.AddWithValue("@ln", cust.LastName);
            cmd1.Parameters.AddWithValue("@em", cust.Email);
            cmd1.Parameters.AddWithValue("@ph", cust.PhoneNumber);
            cmd1.Parameters.AddWithValue("@addr", cust.Address);
            cmd1.ExecuteNonQuery();

            string insertAccount = @"INSERT INTO Accounts (account_id, customer_id, account_type, balance)
                                     VALUES (@aid, @cid, @type, @bal)";
            using var cmd2 = new SqlCommand(insertAccount, conn);
            cmd2.Parameters.AddWithValue("@aid", accNo);
            cmd2.Parameters.AddWithValue("@cid", cust.CustomerId);
            cmd2.Parameters.AddWithValue("@type", accType);
            cmd2.Parameters.AddWithValue("@bal", balance);
            cmd2.ExecuteNonQuery();
        }

        // Returns all accounts with associated customer details
        public List<Account> ListAccounts()
        {
            var accounts = new List<Account>();
            using var conn = DBConnUtil.GetConnection();

            string query = @"SELECT a.account_id, a.account_type, a.balance,
                                    c.customer_id, c.first_name, c.last_name, c.email, c.phone_number, c.address
                             FROM Accounts a
                             JOIN Customers c ON a.customer_id = c.customer_id";

            using var cmd = new SqlCommand(query, conn);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var customer = new Customer(
                    Convert.ToInt64(reader["customer_id"]),
                    reader["first_name"].ToString(),
                    reader["last_name"].ToString(),
                    reader["email"].ToString(),
                    reader["phone_number"].ToString(),
                    reader["address"].ToString()
                );

                string accType = reader["account_type"].ToString().ToLower();
                double balance = Convert.ToDouble(reader["balance"]);

                Account account = accType switch
                {
                    "savings" => new SavingsAccount(balance, customer),
                    "current" => new CurrentAccount(balance, customer),
                    _ => new ZeroBalanceAccount(customer)
                };

                accounts.Add(account);
            }

            return accounts;
        }

        // Returns the balance of the specified account
        public double GetAccountBalance(long accNo)
        {
            using var conn = DBConnUtil.GetConnection();
            string query = "SELECT balance FROM Accounts WHERE account_id = @accNo";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@accNo", accNo);
            return Convert.ToDouble(cmd.ExecuteScalar());
        }

        // Deposits an amount and logs the transaction
        public double Deposit(long accNo, double amount)
        {
            double currentBalance = GetAccountBalance(accNo);
            double newBalance = currentBalance + amount;
            UpdateBalance(accNo, newBalance);
            AddTransaction(accNo, "Deposit", "Cash deposit", amount);
            return newBalance;
        }

        // Withdraws an amount if sufficient balance exists
        public double Withdraw(long accNo, double amount)
        {
            double currentBalance = GetAccountBalance(accNo);
            if (amount > currentBalance)
                throw new Exception("Insufficient balance.");
            double newBalance = currentBalance - amount;
            UpdateBalance(accNo, newBalance);
            AddTransaction(accNo, "Withdraw", "Cash withdrawal", amount);
            return newBalance;
        }

        // Transfers an amount between two accounts and logs both transactions
        public void Transfer(long fromAcc, long toAcc, double amount)
        {
            Withdraw(fromAcc, amount);
            Deposit(toAcc, amount);
            AddTransaction(fromAcc, "Transfer", $"Transferred to {toAcc}", amount);
            AddTransaction(toAcc, "Transfer", $"Received from {fromAcc}", amount);
        }

        // Returns detailed information for a specific account
        public Account GetAccountDetails(long accNo)
        {
            using var conn = DBConnUtil.GetConnection();
            string query = @"SELECT a.account_id, a.account_type, a.balance,
                                    c.customer_id, c.first_name, c.last_name, c.email, c.phone_number, c.address
                             FROM Accounts a
                             JOIN Customers c ON a.customer_id = c.customer_id
                             WHERE a.account_id = @accNo";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@accNo", accNo);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                var customer = new Customer(
                    Convert.ToInt64(reader["customer_id"]),
                    reader["first_name"].ToString(),
                    reader["last_name"].ToString(),
                    reader["email"].ToString(),
                    reader["phone_number"].ToString(),
                    reader["address"].ToString()
                );

                string accType = reader["account_type"].ToString().ToLower();
                double balance = Convert.ToDouble(reader["balance"]);

                return accType switch
                {
                    "savings" => new SavingsAccount(balance, customer),
                    "current" => new CurrentAccount(balance, customer),
                    _ => new ZeroBalanceAccount(customer)
                };
            }

            throw new Exception("Account not found.");
        }

        // Returns all transactions for an account within a date range
        public List<Transaction> GetTransactions(long accNo, DateTime fromDate, DateTime toDate)
        {
            var transactions = new List<Transaction>();
            using var conn = DBConnUtil.GetConnection();

            string query = @"SELECT * FROM Transactions 
                             WHERE account_id = @accNo 
                               AND transaction_date BETWEEN @from AND @to";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@accNo", accNo);
            cmd.Parameters.AddWithValue("@from", fromDate);
            cmd.Parameters.AddWithValue("@to", toDate);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                transactions.Add(new Transaction
                {
                    TransactionId = Convert.ToInt64(reader["transaction_id"]),
                    AccountNumber = Convert.ToInt64(reader["account_id"]),
                    TransactionType = reader["transaction_type"].ToString(),
                    Description = reader["description"].ToString(),
                    TransactionAmount = Convert.ToDouble(reader["amount"]),
                    TransactionDate = Convert.ToDateTime(reader["transaction_date"])
                });
            }

            return transactions;
        }

        // Updates the balance for a given account
        private void UpdateBalance(long accNo, double newBalance)
        {
            using var conn = DBConnUtil.GetConnection();
            string query = "UPDATE Accounts SET balance = @bal WHERE account_id = @accNo";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@bal", newBalance);
            cmd.Parameters.AddWithValue("@accNo", accNo);
            cmd.ExecuteNonQuery();
        }

        // Adds a new transaction record
        private void AddTransaction(long accNo, string type, string desc, double amount)
        {
            using var conn = DBConnUtil.GetConnection();
            string insert = @"INSERT INTO Transactions (account_id, transaction_type, description, amount, transaction_date) 
                              VALUES (@accNo, @type, @desc, @amt, @date)";

            using var cmd = new SqlCommand(insert, conn);
            cmd.Parameters.AddWithValue("@accNo", accNo);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@desc", desc);
            cmd.Parameters.AddWithValue("@amt", amount);
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.ExecuteNonQuery();
        }
    }
}