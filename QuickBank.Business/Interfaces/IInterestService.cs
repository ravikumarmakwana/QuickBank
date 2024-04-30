using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Interfaces
{
    public interface IInterestService
    {
        Task<List<ComputedInterest>> CalculateInterestAsync(
            List<Account> accounts,
            DateTime fromDate,
            DateTime toDate);

        double CalculateInterestForRegularFD(FixedDeposit fixedDeposit);
        double CalculateInterestForCumulativeFD(FixedDeposit fixedDeposit);
        double CalculateInterestForNonCumulativeFD(FixedDeposit fixedDeposit);
    }
}
