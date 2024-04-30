using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class AccountDto
    {
        public long AccountId { get; set; }
        public long CustomerId { get; set; }

        public int AccountTypeId { get; set; }
        public string AccountType { get; set; }

        public string AccountNumber { get; set; }
        public string IFSC { get; set; }
        public AccountStatus AccountStatus { get; set; }
        public double TotalBalance { get; set; }
        public DateTime CreatedOn { get; set; }
        public string FullName { get; set; }
    }
}
