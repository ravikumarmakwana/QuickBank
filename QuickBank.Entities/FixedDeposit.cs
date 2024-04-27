using QuickBank.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("FixedDeposits")]
    public class FixedDeposit
    {
        [Key]
        public long FixedDepositId { get; set; }

        public Account Account { get; set; }
        [ForeignKey(nameof(Account))]
        public long AccountId { get; set; }

        public FixedDepositType FixedDepositType { get; set; }
        [ForeignKey(nameof(FixedDepositType))]
        public long FixedDepositTypeId { get; set; }

        public double PrincipalAmount { get; set; }
        public double InterestedAmount { get; set; }

        public DateTime? LastEarnedDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
        public FDUserPerference UserPerference { get; set; }
    }
}
