using QuickBank.Models;

namespace QuickBank.Business.Helpers
{
    public class TimeDurationService
    {
        private readonly List<Duration> _quarters = new List<Duration>();
        private readonly int _currentYear = DateTime.Now.Year;

        public TimeDurationService()
        {
            for (int i = 1; i <= 12; i = i + 3)
            {
                _quarters.Add(new Duration
                {
                    FromDate = new DateTime(_currentYear, i, 1),
                    ToDate = new DateTime(_currentYear, i + 2, DateTime.DaysInMonth(_currentYear, i + 2))
                });
            }
        }

        public Duration GetCurrentFinancialYear()
        {
            return new Duration
            {
                FromDate = new DateTime(_currentYear, 1, 1),
                ToDate = new DateTime(_currentYear, 12, DateTime.DaysInMonth(_currentYear, 12))
            };
        }

        public Duration GetFinancialQuarter(int quarter)
        {
            return _quarters[quarter - 1];
        }
    }
}
