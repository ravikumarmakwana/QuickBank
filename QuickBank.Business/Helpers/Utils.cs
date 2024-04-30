using QuickBank.Entities.Enums;

namespace QuickBank.Business.Helpers
{
    public static class Utils
    {
        public static void SetTransactionDates(
            TransactionRange transactionRange, out DateTime localStartDate, out DateTime localEndDate,
            in DateTime? startDate, in DateTime? endDate)
        {
            switch (transactionRange)
            {
                case TransactionRange.LastYear:
                    localStartDate = DateTime.Now.AddYears(-1);
                    localEndDate = DateTime.Now;
                    break;

                case TransactionRange.LastQuarter:
                    localStartDate = DateTime.Now.AddMonths(-3);
                    localEndDate = DateTime.Now;
                    break;

                case TransactionRange.LastMonth:
                    localStartDate = DateTime.Now.AddMonths(-1);
                    localEndDate = DateTime.Now;
                    break;

                case TransactionRange.LastWeek:
                    localStartDate = DateTime.Now.AddDays(-7);
                    localEndDate = DateTime.Now;
                    break;

                case TransactionRange.Last3Days:
                    localStartDate = DateTime.Now.AddDays(-3);
                    localEndDate = DateTime.Now;
                    break;

                case TransactionRange.Custom:
                    if (startDate == null || endDate == null)
                    {
                        throw new InvalidOperationException("StartDate and EndDate is required for Custom Transaction Range");
                    }

                    if (startDate > endDate)
                    {
                        throw new InvalidOperationException("Invalid StartDate and EndDate, StartDate always smaller than EndDate");
                    }

                    localStartDate = (DateTime)startDate;
                    localEndDate = (DateTime)endDate;
                    break;

                default:
                    throw new InvalidOperationException("Invalid Transaction Range");
            }
        }

        public static double CalculateInterest(double interestRate, double amount, double numOfYears)
        {
            return amount * interestRate * numOfYears / 100;
        }
    }
}
