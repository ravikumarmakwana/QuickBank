using FluentValidation;
using QuickBank.Entities.Enums;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
    {
        public const string Required = "{0} is required";
        public const string Invalid = "Invalid {0}";
        public const string RegexForName = "^[a-zA-Z]{2,}$";
        public const string RegexForPhoneNumber = "^[0-9]{10,15}$";
        public const string RegexForEmailAddress = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";

        public RegistrationRequestValidator()
        {
            RuleFor(name => name.FirstName)
                .NotNull()
                .WithMessage(name => string.Format(Required, nameof(name.FirstName)))
                .Matches(RegexForName)
                .WithMessage(name => string.Format(Invalid, nameof(name.FirstName)));

            RuleFor(name => name.MiddleName)
                .NotNull()
                .WithMessage(name => string.Format(Required, nameof(name.MiddleName)))
                .Matches(RegexForName)
                .WithMessage(name => string.Format(Invalid, nameof(name.MiddleName)));

            RuleFor(name => name.LastName)
                .NotNull()
                .WithMessage(name => string.Format(Required, nameof(name.LastName)))
                .Matches(RegexForName)
                .WithMessage(name => string.Format(Invalid, nameof(name.LastName)));

            RuleFor(customer => customer.DateOfBirth)
                .NotEmpty()
                .WithMessage(customer => string.Format(Required, nameof(customer.DateOfBirth)));

            RuleFor(customer => customer.Gender)
                .Must(gender => Enum.IsDefined(typeof(Gender), gender))
                .WithMessage(customer => string.Format(Invalid, nameof(customer.Gender)));

            RuleFor(contactInformation => contactInformation.PhoneNumber)
                .NotNull()
                .WithMessage(contactInformation => string.Format(Required, nameof(contactInformation.PhoneNumber)))
                .Matches(RegexForPhoneNumber)
                .WithMessage(contactInformation => string.Format(Invalid, nameof(contactInformation.PhoneNumber)));

            RuleFor(contactInformation => contactInformation.EmailAddress)
                .NotNull()
                .WithMessage(contactInformation => string.Format(Required, nameof(contactInformation.EmailAddress)))
                .Matches(RegexForEmailAddress)
                .WithMessage(contactInformation => string.Format(Invalid, nameof(contactInformation.EmailAddress)));
        }
    }
}
