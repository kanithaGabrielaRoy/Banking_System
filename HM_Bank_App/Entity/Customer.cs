namespace HM_Bank_App.Entity
{
    public class Customer
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        //  This constructor is required to fix your CS1729 error
        public Customer(long customerId, string firstName, string lastName, string email, string phone, string address)
        {
            CustomerId = customerId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phone;
            Address = address;
        }

        // Optional: parameterless constructor
        public Customer() { }

        public override string ToString()
        {
            return $"{CustomerId}: {FirstName} {LastName}, Email: {Email}, Phone: {PhoneNumber}, Address: {Address}";
        }
    }
}
