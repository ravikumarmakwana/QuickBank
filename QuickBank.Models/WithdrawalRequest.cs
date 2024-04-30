namespace QuickBank.Models
{
    public class WithdrawalRequest
    {
        public long AccountId { get; set; }
        public double WithdrawalAmount { get; set; }
        public string Particulars { get; set; }
    }
}
