using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class DepositRequestValidator : AbstractValidator<DepositRequest>
    {
        public const string InvalidAccountId = "Invalid AccountId";
        public const string InvalidAmount = "Amount should be greater than 0";

        public DepositRequestValidator()
        {
            RuleFor(deposit => deposit.AccountId)
                .GreaterThan(0)
                .WithMessage(InvalidAccountId);

            RuleFor(deposit => deposit.DepositAmount)
                .GreaterThan(0)
                .WithMessage(InvalidAmount);
        }
    }
}
