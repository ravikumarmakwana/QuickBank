using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class UpdateAddressRequestValidator : AbstractValidator<UpdateAddressRequest>
    {
        public const string Required = "{0} is required";
        public const string Invalid = "Invalid {0}";

        public const string RegexForPinCode = "^[0-9]{6}$";

        public UpdateAddressRequestValidator()
        {
            RuleFor(address => address.AddressLine1)
                .NotNull()
                .WithMessage(address => string.Format(Required, nameof(address.AddressLine1)));

            RuleFor(address => address.AddressLine2)
                .NotNull()
                .WithMessage(address => string.Format(Required, nameof(address.AddressLine2)));

            RuleFor(address => address.City)
                .NotNull()
                .WithMessage(address => string.Format(Required, nameof(address.City)));

            RuleFor(address => address.State)
                .NotNull()
                .WithMessage(address => string.Format(Required, nameof(address.State)));

            RuleFor(address => address.Country)
                .NotNull()
                .WithMessage(address => string.Format(Required, nameof(address.Country)));

            RuleFor(address => address.PinCode)
                .NotNull()
                .WithMessage(address => string.Format(Required, nameof(address.PinCode)))
                .Matches(RegexForPinCode)
                .WithMessage(address => string.Format(Invalid, nameof(address.PinCode)));
        }
    }
}
