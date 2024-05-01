using FluentValidation;
using QuickBank.Models;

namespace QuickBank.API.Validators
{
    public class FundTransferRequestValidator : AbstractValidator<FundTransferRequest>
    {
        public const string Invalid = "Invalid {0}";
        public const string InvalidTransactionAmount = "Amount should be greater than 0";

        public FundTransferRequestValidator()
        {
            RuleFor(fundTransfer => fundTransfer.DebitAccountId)
                .GreaterThan(0)
                .WithMessage(fundTransfer => string.Format(Invalid, nameof(fundTransfer.DebitAccountId)));

            RuleFor(fundTransfer => fundTransfer.CreditAccountId)
                .GreaterThan(0)
                .WithMessage(fundTransfer => string.Format(Invalid, nameof(fundTransfer.CreditAccountId)));

            RuleFor(fundTransfer => fundTransfer.TransactionAmount)
                .GreaterThan(0)
                .WithMessage(InvalidTransactionAmount);
        }
    }
}
