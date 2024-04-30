namespace QuickBank.Models
{
    public class ComputedDailyInterest
    {
        public DateTime Date { get; set; }
        public double Interest { get; set; }
        public double EndOfDayClosingBalance { get; set; }
    }
}
