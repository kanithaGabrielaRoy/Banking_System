using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;


    namespace HM_Bank_App.Util
    {
        public class DBPropertyUtil
        {
            public static string GetConnectionString(string jsonPath)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory()) // Gets the project directory
                    .AddJsonFile(jsonPath)                        
                    .Build();

                return config.GetConnectionString("DefaultConnection");
            }
        }
    }
