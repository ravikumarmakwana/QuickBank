using QuickBank.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }

        public Account Account { get; set; }
        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }

        public DateTime TransactionDate { get; set; }
        public double Amount { get; set; }
        public double TotalBalance { get; set; }
        public string Particulars { get; set; }
        public TransactionType TransactionType { get; set; }
    }
}
