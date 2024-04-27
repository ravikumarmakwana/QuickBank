using Microsoft.EntityFrameworkCore.Metadata;
using QuickBank.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
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

        public List<Address> Addresses { get; set; }
        public List<Account> Accounts { get; set; }

        public ApplicationUser User { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
    }
}
