using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickBank.Entities
{
    [Table("FixedDepositTypes")]
    public class FixedDepositType
    {
        [Key]
        public long FixedDepositTypeId { get; set; }

        public string TypeName { get; set; }
        public double InterestRate { get; set; }

        public List<FixedDeposit> FixedDeposits { get; set; }
    }
}
