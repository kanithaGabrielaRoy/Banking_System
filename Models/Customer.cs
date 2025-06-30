using System;

namespace HM_Bank_CoreApp.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"Customer: {FirstName} {LastName}, Email: {Email}, Phone: {PhoneNumber}, Address: {Address}";
        }
    }
}


