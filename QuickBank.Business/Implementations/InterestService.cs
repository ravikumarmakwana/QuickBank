using QuickBank.Business.Helpers;
using QuickBank.Business.Interfaces;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Implementations
{
    public class InterestService : IInterestService
    {
        private readonly ITransactionRepository _transactionRepository;

        public InterestService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<ComputedInterest>> CalculateInterestAsync(
            List<Account> accounts,
            DateTime fromDate,
            DateTime toDate)
        {
            List<ComputedInterest> computedInterests = new List<ComputedInterest>();
            foreach (var account in accounts)
            {
                computedInterests.Add(await CalculateInterestAsync(account, fromDate, toDate));
            }

            return computedInterests;
        }

        private double CalculateInterest(
            double openingBalance,
            List<Transaction> transactions,
            DateTime fromDate,
            DateTime toDate,
            double interestRate)
        {
            if (!transactions.Any())
            {
                var interestCalculationDurationInYears = (double)(toDate - fromDate).Days / 365;
                return Utils.CalculateInterest(
                    interestRate,
                    openingBalance,
                    interestCalculationDurationInYears
                );
            }

            double interestAmount = 0;

            for (var date = fromDate; date <= toDate; date = date.AddDays(1))
            {
                var transactionsForDay = GetTransactionsForDay(date, transactions);
                var openingBalanceForDay = GetOpeningBalanceForDay(transactions, date);

                var dailyInterest = ComputeDailyInterestForOneDay(
                    openingBalanceForDay,
                    transactionsForDay,
                    interestRate);

                interestAmount += dailyInterest;
            }

            return interestAmount;
        }

        private double GetOpeningBalanceForDay(List<Transaction> transactions, DateTime date)
        {
            return transactions
                .LastOrDefault(_ => _.TransactionDate < date)?
                .TotalBalance ?? 0;
        }

        private List<Transaction> GetTransactionsForDay(DateTime date, List<Transaction> transactions)
        {
            return transactions
                .Where(_ => _.TransactionDate.Date == date)
                .ToList();
        }

        private double ComputeDailyInterestForOneDay(
            double openingBalanceForDay,
            List<Transaction> transactionForDay,
            double interestRate)
        {
            double minBalanceAcrossAllTransactions = 0;
            if(transactionForDay.Any())
            {
                minBalanceAcrossAllTransactions = transactionForDay
                    .OrderBy(_ => _.TransactionDate)
                    .Min(_ => _.TotalBalance);
            }

            var minBalanceForDay = Math.Min(
                openingBalanceForDay,
                minBalanceAcrossAllTransactions);

            return Utils.CalculateInterest(interestRate, minBalanceForDay, (double)1 / 365);
        }

        private async Task<ComputedInterest> CalculateInterestAsync(
            Account account,
            DateTime fromDate,
            DateTime toDate)
        {
            var interestRate = account.AccountType.InterestRate;

            var effectiveStartDate = GetEffectiveInterestCalculationStartDate(
                account.CreatedOn.Date,
                fromDate
            );

            var transactions = await _transactionRepository.GetTransactionsAsync(
                account.AccountId,
                effectiveStartDate,
                toDate);

            var interestAmount = CalculateInterest(
                account.Balance,
                transactions,
                fromDate,
                toDate,
                interestRate);

            return new ComputedInterest
            {
                Account = account,
                InterestAmount = interestAmount
            };
        }

        private static DateTime GetEffectiveInterestCalculationStartDate(
            DateTime accountOpeningDate,
            DateTime interestCalculationPeriodStartDate)
        {
            return accountOpeningDate > interestCalculationPeriodStartDate
                ? accountOpeningDate
                : interestCalculationPeriodStartDate;
        }

        public double CalculateInterestForRegularFD(FixedDeposit fixedDeposit)
        {
            var numberOfDays = (fixedDeposit.EndDate.Date - fixedDeposit.StartDate.Date).Days;
            return Utils.CalculateInterest(fixedDeposit.FixedDepositType.InterestRate, fixedDeposit.PrincipalAmount, (double)numberOfDays / 365);
        }

        public double CalculateInterestForCumulativeFD(FixedDeposit fixedDeposit)
        {
            var numberOfDays = (fixedDeposit.EndDate - (DateTime)(fixedDeposit.LastEarnedDate == null ? fixedDeposit.StartDate : fixedDeposit.LastEarnedDate)).Days;
            numberOfDays = numberOfDays < 0 ? 0 : numberOfDays;
            var effectivePrincipalAmount = fixedDeposit.PrincipalAmount + fixedDeposit.InterestedAmount;
            return Utils.CalculateInterest(fixedDeposit.FixedDepositType.InterestRate, effectivePrincipalAmount, (double)numberOfDays / 365);
        }

        public double CalculateInterestForNonCumulativeFD(FixedDeposit fixedDeposit)
        {
            var numberOfDays = (fixedDeposit.EndDate - (DateTime)(fixedDeposit.LastEarnedDate == null ? fixedDeposit.StartDate : fixedDeposit.LastEarnedDate)).Days;
            numberOfDays = numberOfDays < 0 ? 0 : numberOfDays;
            return Utils.CalculateInterest(fixedDeposit.FixedDepositType.InterestRate, fixedDeposit.PrincipalAmount, (double)numberOfDays / 365);
        }
    }
}
