using System.Data.SqlClient;


namespace HM_Bank_App.Util
{
    public class DBConnUtil
    {
        private static string connectionString = "Server=localhost;Database=HMBank;Trusted_Connection=True;";

        public static SqlConnection GetConnection()
        {
            try
            {
                var conn = new SqlConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection failed: " + ex.Message);
                throw;
            }
        }
    }
}