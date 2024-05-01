using AutoMapper;
using QuickBank.Business.Constants;
using QuickBank.Business.Exceptions;
using QuickBank.Business.Helpers;
using QuickBank.Business.Interfaces;
using QuickBank.Business.ServiceValidators;
using QuickBank.Data.Interfaces;
using QuickBank.Entities.Enums;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Implementations
{
    public class FixedDepositService : IFixedDepositService
    {
        private readonly IFixedDepositRepository _fixedDepositRepository;
        private readonly ITransactionService _transactionService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IFixedDepositTypeRepository _fixedDepositTypeRepository;
        private readonly IInterestService _interestService;
        private readonly ICustomerServiceValidator _customerServiceValidator;
        private readonly IAccountServiceValidator _accountServiceValidator;
        private readonly IFixedDepositServiceValidator _fixedDepositServiceValidator;

        private readonly IMapper _mapper;

        public FixedDepositService(
            IFixedDepositRepository fixedDepositRepository,
            ITransactionService transactionService,
            ICustomerRepository customerRepository,
            IAccountRepository accountRepository,
            IFixedDepositTypeRepository fixedDepositTypeRepository,
            IInterestService interestService,
            ICustomerServiceValidator customerServiceValidator,
            IAccountServiceValidator accountServiceValidator,
            IFixedDepositServiceValidator fixedDepositServiceValidator,
            IMapper mapper)
        {
            _fixedDepositRepository = fixedDepositRepository;
            _transactionService = transactionService;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _fixedDepositTypeRepository = fixedDepositTypeRepository;
            _interestService = interestService;
            _customerServiceValidator = customerServiceValidator;
            _accountServiceValidator = accountServiceValidator;
            _fixedDepositServiceValidator = fixedDepositServiceValidator;

            _mapper = mapper;
        }

        public async Task<FixedDepositDto> AddFixedDeposit(FixedDepositRequest fixedDepositRequest)
        {
            var account = await _accountRepository.GetAccountByAccountIdAsync(fixedDepositRequest.AccountId);
            _accountServiceValidator.ValidateAccountExists(fixedDepositRequest.AccountId, account);

            var fixedDepositType = await _fixedDepositTypeRepository.GetFixedDepositTypeByIdAsync(fixedDepositRequest.FixedDepositTypeId);
            _fixedDepositServiceValidator.ValidateFixedDepositTypeExists(fixedDepositRequest.FixedDepositTypeId, fixedDepositType);

            var fixedDeposit = _mapper.Map<FixedDeposit>(fixedDepositRequest);

            fixedDeposit.IsActive = true;

            await WithdrawInterestAmountAsync(fixedDepositRequest, account, fixedDepositType, fixedDeposit);
            await _fixedDepositRepository.AddFixedDeposit(fixedDeposit);

            fixedDeposit.FixedDepositType = fixedDepositType;
            return _mapper.Map<FixedDepositDto>(fixedDeposit);
        }

        private async Task WithdrawInterestAmountAsync(FixedDepositRequest fixedDepositRequest, Account account, FixedDepositType fixedDepositType, FixedDeposit fixedDeposit)
        {
            if (account.Balance < fixedDeposit.PrincipalAmount)
            {
                throw new InvalidTransactionException(
                    $"Account with AcountId: {account.AccountId}, doesn't has enough balance for applying Fixed Deposit"
                    );
            }
            var withdrawalRequest = new WithdrawalRequest
            {
                AccountId = fixedDeposit.AccountId,
                Particulars = $"Applied for {fixedDepositType.TypeName} Fixed Deposit for {fixedDepositRequest.StartDate.ToShortDateString()} to {fixedDepositRequest.EndDate.ToShortDateString()}",
                WithdrawalAmount = fixedDeposit.PrincipalAmount
            };

            await _transactionService.Withdrawal(withdrawalRequest);
        }

        public async Task CloseFixedDepositById(long fixedDepositId)
        {
            var fixedDeposit = await _fixedDepositRepository.GetFixedDepositById(fixedDepositId);
            _fixedDepositServiceValidator.ValidateFixedDepositExists(fixedDepositId, fixedDeposit);

            await CloseFixedDeposit(fixedDeposit);
        }

        public async Task CloseFixedDeposit(FixedDeposit fixedDeposit)
        {
            double interestAmount = 0;
            if (fixedDeposit.DoesMature())
            {
                switch (fixedDeposit.FixedDepositType.FixedDepositTypeId)
                {
                    case 1:
                        interestAmount = _interestService.CalculateInterestForRegularFD(fixedDeposit);
                        break;

                    case 2:
                        interestAmount = _interestService.CalculateInterestForCumulativeFD(fixedDeposit);
                        break;

                    case 3:
                        interestAmount = _interestService.CalculateInterestForNonCumulativeFD(fixedDeposit);
                        break;
                }
            }

            fixedDeposit.IsActive = false;
            fixedDeposit.InterestedAmount += interestAmount;

            await DepositInterestAmountAsync(
                fixedDeposit.AccountId,
                fixedDeposit.PrincipalAmount + interestAmount,
                $"Close {fixedDeposit.FixedDepositType.TypeName} Fixed Deposit");

            await _fixedDepositRepository.UpdateFixedDeposit(fixedDeposit);
        }

        public async Task RenewFixedDeposit(FixedDeposit fixedDeposit)
        {
            await CloseFixedDeposit(fixedDeposit);

            var fixedDepositRequest = new FixedDepositRequest
            {
                AccountId = fixedDeposit.AccountId,
                FixedDepositTypeId = fixedDeposit.FixedDepositTypeId,
                PrincipalAmount = fixedDeposit.PrincipalAmount,
                UserPerference = fixedDeposit.UserPerference,
                StartDate = DateTime.Now,
                EndDate = DateTime.UtcNow.AddDays((fixedDeposit.EndDate - fixedDeposit.StartDate).Days)
            };

            await AddFixedDeposit(fixedDepositRequest);
        }

        public async Task<FixedDepositDto> GetFixedDepositById(long fixedDepositId)
        {
            var fixedDeposit = await _fixedDepositRepository.GetFixedDepositById(fixedDepositId);

            _fixedDepositServiceValidator.ValidateFixedDepositExists(fixedDepositId, fixedDeposit);
            return _mapper.Map<FixedDepositDto>(fixedDeposit);
        }

        public async Task<List<FixedDepositDto>> GetAllFixedDepositForAccount(long accountId)
        {
            var account = await _accountRepository.GetAccountByAccountIdAsync(accountId);
            _accountServiceValidator.ValidateAccountExists(accountId, account);

            var fixedDeposits = await _fixedDepositRepository.GetAllFixedDepositForAccounts(new List<long> { accountId });
            return _mapper.Map<List<FixedDepositDto>>(fixedDeposits);
        }

        public async Task<List<FixedDepositDto>> GetAllFixedDepositForCustomer(long customerId)
        {
            await ValidateCustomerExists(customerId);

            var accounts = await _accountRepository.GetAccountsByCustomerIdAsync(customerId);
            var fixedDeposits = await _fixedDepositRepository.GetAllFixedDepositForAccounts(accounts.Select(_ => _.AccountId).ToList());

            return _mapper.Map<List<FixedDepositDto>>(fixedDeposits);
        }

        public async Task ComputeFixedDeposits()
        {
            var fixedDeposits = await _fixedDepositRepository.GetAllActiveFixedDeposits();
            await CalculateAndStoreInterestAsync(GetCumulativeAndNonCumulativeFixedDeposits(fixedDeposits));

            foreach (var fixedDeposit in fixedDeposits.Where(fd => fd.DoesMature()))
            {
                if (fixedDeposit.UserPerference == FDUserPerference.CloseAndDeposit)
                {
                    await CloseFixedDeposit(fixedDeposit);
                }
                else if (fixedDeposit.UserPerference == FDUserPerference.Renew)
                {
                    await RenewFixedDeposit(fixedDeposit);
                }
                else
                {
                    fixedDeposit.IsActive = false;
                    await _fixedDepositRepository.UpdateFixedDeposit(fixedDeposit);
                }
            }
        }

        private async Task ValidateCustomerExists(long customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            _customerServiceValidator.ValidateCustomerExists(customerId, customer);
        }

        private async Task CalculateAndStoreInterestAsync(List<FixedDeposit> fixedDeposits)
        {
            foreach (var fixedDeposit in fixedDeposits)
            {
                var effectiveStartDate = GetEffectiveInterestCalculationStartDate(fixedDeposit);

                if (DoesQuarterComplete(effectiveStartDate))
                {
                    var principalAmount = GetEffectivePrincipalAmount(fixedDeposit);

                    var interestAmount = Utils.CalculateInterest(
                        fixedDeposit.FixedDepositType.InterestRate, principalAmount,
                        (double)(DateTime.Now - effectiveStartDate).Days / 365
                    );

                    fixedDeposit.InterestedAmount += interestAmount;
                    fixedDeposit.LastEarnedDate = DateTime.Now;

                    await _fixedDepositRepository.UpdateFixedDeposit(fixedDeposit);

                    if (fixedDeposit.FixedDepositType.TypeName == APIConstants.NonCumulative)
                    {
                        await DepositInterestAmountAsync(
                            fixedDeposit.AccountId,
                            interestAmount,
                            "Interest amount for Non-Cumulative Fixed Deposit");
                    }
                }
            }
        }

        private double GetEffectivePrincipalAmount(FixedDeposit fixedDeposit)
        {
            return fixedDeposit.FixedDepositType.TypeName == APIConstants.Cumulative
                            ? fixedDeposit.PrincipalAmount + fixedDeposit.InterestedAmount
                            : fixedDeposit.PrincipalAmount;
        }

        private async Task DepositInterestAmountAsync(long accountId, double amount, string particulars)
        {
            var depositRequest = new DepositRequest
            {
                AccountId = accountId,
                DepositAmount = amount,
                Particulars = particulars
            };

            await _transactionService.Deposit(depositRequest);
        }

        private DateTime GetEffectiveInterestCalculationStartDate(FixedDeposit fixedDeposit)
        {
            return (DateTime)(fixedDeposit.InterestedAmount == 0 ? fixedDeposit.StartDate : fixedDeposit.LastEarnedDate);
        }

        private bool DoesQuarterComplete(DateTime date)
        {
            return DateTime.Now.AddMonths(-3).Date >= date.Date;
        }

        private List<FixedDeposit> GetCumulativeAndNonCumulativeFixedDeposits(List<FixedDeposit> fixedDeposits)
        {
            return fixedDeposits
                .Where(_ => _.FixedDepositType.TypeName == APIConstants.Cumulative || _.FixedDepositType.TypeName == APIConstants.NonCumulative)
                .ToList();
        }
    }
}