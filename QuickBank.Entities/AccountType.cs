using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("AccountTypes")]
    public class AccountType
    {
        [Key]
        public long AccountTypeId { get; set; }

        public string Name { get; set; }
        public double InterestRate { get; set; }
        public int AvailableFreeCheckbook { get; set; }
        public double AnnualDebitCardCharge { get; set; }
        public double MinimumRequiredBalance { get; set; }
        public int NumberOfDaysToInactive { get; set; }
        public int NumberOfDaysToDormant { get; set; }

        public List<Account> Accounts { get; set; }
    }
}
