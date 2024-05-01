using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class CustomerPIIModelValidator : AbstractValidator<CustomerPIIModel>
    {
        public const string Required = "{0} is required";
        public const string Invalid = "Invalid {0}";

        public const string RegexForAadharNumber = "^[0-9]{12}$";
        public const string RegexForPAN = "^[0-9a-zA-Z]{10}$";

        public CustomerPIIModelValidator()
        {
            RuleFor(pII => pII.AadharNumber)
                .NotNull()
                .WithMessage(pII => string.Format(Required, nameof(pII.AadharNumber)))
                .Matches(RegexForAadharNumber)
                .WithMessage(pII => string.Format(Invalid, nameof(pII.AadharNumber)));

            RuleFor(pII => pII.PAN)
                .NotNull()
                .WithMessage(pII => string.Format(Required, nameof(pII.PAN)))
                .Matches(RegexForPAN)
                .WithMessage(pII => string.Format(Invalid, nameof(pII.PAN)));
        }
    }
}
