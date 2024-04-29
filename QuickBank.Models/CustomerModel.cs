using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class CustomerModel
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }

        public string AadharNumber { get; set; }
        public string PAN { get; set; }

        public CustomerStatus CustomerStatus { get; set; }

        public List<AddressModel> Addresses { get; set; }

        public long UserId { get; set; }
    }
}
