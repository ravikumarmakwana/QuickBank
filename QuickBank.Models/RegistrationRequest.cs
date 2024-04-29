using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
    }
}
