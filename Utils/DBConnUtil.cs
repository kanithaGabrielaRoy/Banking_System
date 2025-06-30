using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HM_Bank_CoreApp.Utils
{
    public static class DBConnUtil
    {
        public static SqlConnection GetConnection()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");
            return new SqlConnection(connectionString);
        }
    }
}
