using QuickBank.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [Key]
        public long AccountId { get; set; }

        public Customer Customer { get; set; }
        [ForeignKey(nameof(Customer))]
        public long CustomerId { get; set; }

        public AccountType AccountType { get; set; }
        [ForeignKey(nameof(AccountType))]
        public long AccountTypeId { get; set; }

        public string AccountNumber { get; set; }
        public string IFSC { get; set; }

        public AccountStatus AccountStatus { get; set; }
        public double Balance { get; set; }
        public DateTime CreatedOn { get; set; }

        public List<Transaction> Transactions { get; set; }
        public List<FixedDeposit> FixedDeposits { get; set; }
    }
}
