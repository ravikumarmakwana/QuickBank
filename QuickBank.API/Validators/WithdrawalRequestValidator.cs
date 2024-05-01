using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class WithdrawalRequestValidator : AbstractValidator<WithdrawalRequest>
    {
        public const string InvalidAccountId = "Invalid AccountId";
        public const string InvalidAmount = "Amount should be greater than 0";

        public WithdrawalRequestValidator()
        {
            RuleFor(withdrawal => withdrawal.AccountId)
                .GreaterThan(0)
                .WithMessage(InvalidAccountId);

            RuleFor(withdrawal => withdrawal.WithdrawalAmount)
                .GreaterThan(0)
                .WithMessage(InvalidAmount);
        }
    }
}
