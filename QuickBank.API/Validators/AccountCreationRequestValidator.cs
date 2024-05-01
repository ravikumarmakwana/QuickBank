using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class AccountCreationRequestValidator : AbstractValidator<AccountCreationRequest>
    {
        public const string Required = "{0} is required";
        public const string Invalid = "Invalid {0}";

        public const string RegexForIFSC = "^[a-zA-Z0-9]{11}$";

        public AccountCreationRequestValidator()
        {
            RuleFor(account => account.CustomerId)
                .GreaterThan(0)
                .WithMessage(account => string.Format(Invalid, nameof(account.CustomerId)));

            RuleFor(account => account.AccountTypeId)
                .GreaterThan(0)
                .WithMessage(account => string.Format(Invalid, nameof(account.AccountTypeId)));

            RuleFor(account => account.IFSC)
                .NotNull()
                .WithMessage(account => string.Format(Required, nameof(account.IFSC)))
                .Matches(RegexForIFSC)
                .WithMessage(account => string.Format(Invalid, nameof(account.IFSC)));
        }
    }
}
