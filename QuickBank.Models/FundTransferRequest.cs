namespace QuickBank.Models
{
    public class FundTransferRequest
    {
        public long DebitAccountId { get; set; }
        public long CreditAccountId { get; set; }
        public double TransactionAmount { get; set; }
        public string Particulars { get; set; }
    }
}
