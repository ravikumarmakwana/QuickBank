using FluentValidation;
using QuickBank.Entities.Enums;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class FixedDepositRequestValidator : AbstractValidator<FixedDepositRequest>
    {
        public const string Invalid = "Invalid {0}";
        public const string Required = "{0} is required";

        public const string InvalidPrincipalAmount = "PrincipalAmount should be greater than 0";
        public const string InvalidTenureOfFixedDeposit = "Invalid Tenure of Fixed Deposit, minimum 7 day required";

        public FixedDepositRequestValidator()
        {
            RuleFor(fixedDeposit => fixedDeposit.AccountId)
                .GreaterThan(0)
                .WithMessage(fixedDeposit => string.Format(Invalid, nameof(fixedDeposit.AccountId)));

            RuleFor(fixedDeposit => fixedDeposit.PrincipalAmount)
                .GreaterThan(0)
                .WithMessage(InvalidPrincipalAmount);

            RuleFor(fixedDeposit => fixedDeposit.FixedDepositTypeId)
                .GreaterThan(0)
                .WithMessage(fixedDeposit => string.Format(Invalid, nameof(fixedDeposit.FixedDepositTypeId)));

            RuleFor(fixedDeposit => fixedDeposit.UserPerference)
                .Must(userPerference => Enum.IsDefined(typeof(FDUserPerference), userPerference))
                .WithMessage(fixedDeposit => string.Format(Invalid, nameof(fixedDeposit.UserPerference)));

            RuleFor(fixedDeposit => fixedDeposit.StartDate)
                .NotEmpty()
                .WithMessage(fixedDeposit => string.Format(Required, nameof(fixedDeposit.StartDate)));

            RuleFor(fixedDeposit => fixedDeposit.EndDate)
                .NotEmpty()
                .WithMessage(fixedDeposit => string.Format(Required, nameof(fixedDeposit.EndDate)));

            RuleFor(fixedDeposit => fixedDeposit)
                .Must(fixedDeposit => (fixedDeposit.EndDate - fixedDeposit.StartDate).Days >= 7)
                .WithMessage(InvalidTenureOfFixedDeposit);
        }
    }
}
