using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class UpdateEmailRequestValidator : AbstractValidator<UpdateEmailRequest>
    {
        public const string Required = "{0} is required";
        public const string Invalid = "Invalid {0}";

        public const string RegexForEmailAddress = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";

        public UpdateEmailRequestValidator()
        {
            RuleFor(customer => customer.EmailAddress)
                .NotNull()
                .WithMessage(customer => string.Format(Required, nameof(customer.EmailAddress)))
                .Matches(RegexForEmailAddress)
                .WithMessage(customer => string.Format(Invalid, nameof(customer.EmailAddress)));
        }
    }
}
