using QuickBank.Entities;

namespace QuickBank.Models
{
    public class ComputedInterest
    {
        public double InterestAmount { get; set; }
        public Account Account { get; set; }
    }
}
