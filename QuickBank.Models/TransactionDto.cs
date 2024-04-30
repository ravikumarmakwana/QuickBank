using QuickBank.Entities.Enums;

namespace QuickBank.Models
{
    public class TransactionDto
    {
        public long TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TotalBalance { get; set; }
        public string Particulars { get; set; }
    }
}
