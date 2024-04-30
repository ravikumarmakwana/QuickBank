using FluentValidation;
using QuickBank.Entities.Enums;
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

    public class UpdateEmailAddressRequestValidator : AbstractValidator<UpdateEmailRequest>
    {
        public const string Required = "{0} is required";
        public const string Invalid = "Invalid {0}";

        public const string RegexForEmailAddress = @"^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$";

        public UpdateEmailAddressRequestValidator()
        {
            RuleFor(customer => customer.EmailAddress)
                .NotNull()
                .WithMessage(customer => string.Format(Required, nameof(customer.EmailAddress)))
                .Matches(RegexForEmailAddress)
                .WithMessage(customer => string.Format(Invalid, nameof(customer.EmailAddress)));
        }
    }

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
