using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class FixedDepositRequest
    {
        public long AccountId { get; set; }
        public double PrincipalAmount { get; set; }
        public long FixedDepositTypeId { get; set; }
        public FDUserPerference UserPerference { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
