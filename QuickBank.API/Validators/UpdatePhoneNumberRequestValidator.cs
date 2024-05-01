using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class UpdatePhoneNumberRequestValidator : AbstractValidator<UpdatePhoneNumberRequest>
    {
        public const string Required = "{0} is required";
        public const string Invalid = "Invalid {0}";

        public const string RegexForPhoneNumber = "^[0-9]{10,15}$";

        public UpdatePhoneNumberRequestValidator()
        {
            RuleFor(customer => customer.PhoneNumber)
                .NotNull()
                .WithMessage(customer => string.Format(Required, nameof(customer.PhoneNumber)))
                .Matches(RegexForPhoneNumber)
                .WithMessage(customer => string.Format(Invalid, nameof(customer.PhoneNumber)));
        }
    }
}
