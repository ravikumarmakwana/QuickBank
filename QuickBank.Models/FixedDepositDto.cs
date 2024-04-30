using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class FixedDepositDto
    {
        public long FixedDepositId { get; set; }
        public long AccountId { get; set; }

        public int FixedDepositTypeId { get; set; }
        public string FixedDepositType { get; set; }

        public double InterestRate { get; set; }

        public double PrincipalAmount { get; set; }
        public double InterestedAmount { get; set; }

        public DateTime? LastEarendDate { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }
        public FDUserPerference UserPerference { get; set; }
    }
}
