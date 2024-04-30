using QuickBank.Entities.Enums;
using QuickBank.Entities;
using QuickBank.Models;
using AutoMapper;
using QuickBank.Business.ServiceValidators;
using QuickBank.Data.Interfaces;
using QuickBank.Business.Helpers;
using QuickBank.Business.Interfaces;

namespace QuickBank.Business.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountServiceValidator _accountServiceValidator;
        private readonly ICustomerServiceValidator _customerServiceValidator;
        private readonly IMapper _mapper;

        public AccountService(
            IAccountRepository accountRepository, 
            ICustomerRepository customerRepository, 
            IAccountServiceValidator accountServiceValidator, 
            ICustomerServiceValidator customerServiceValidator, 
            IMapper mapper)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _accountServiceValidator = accountServiceValidator;
            _customerServiceValidator = customerServiceValidator;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccountAsync(AccountCreationRequest accountCreationRequest)
        {
            var account = _mapper.Map<Account>(accountCreationRequest);

            await ValidateCustomerExistsAsync(account.CustomerId);

            await _accountServiceValidator.DoesIFSCExists(account.IFSC);
            await _accountServiceValidator.DoesAccountTypeExists(account.AccountTypeId);

            var accounts = await _accountRepository.GetAllAccounts();
            account.AccountNumber = Generator.GenerateNewAccountNumber(
                accounts.Select(_ => _.AccountNumber).ToList()
                );
            account.AccountStatus = AccountStatus.Active;
            account.CreatedOn = DateTime.Now;

            await _accountRepository.CreateAccountAsync(account);

            return _mapper.Map<AccountDto>(account);
        }

        public async Task CloseAccountByAccountIdAsync(long accountId)
        {
            var account = await GetAccountAsync(accountId);
            account.AccountStatus = AccountStatus.Closed;
            await _accountRepository.UpdateAccountAsync(account);
        }

        public async Task<AccountDto> GetAccountByAccountIdAsync(long accountId)
        {
            var account = await GetAccountAsync(accountId);
            return _mapper.Map<AccountDto>(account);
        }

        public async Task<List<AccountDto>> GetAccountsByCustomerIdAsync(long customerId)
        {
            await ValidateCustomerExistsAsync(customerId);

            var accounts = await _accountRepository.GetAccountsByCustomerIdAsync(customerId);
            return _mapper.Map<List<AccountDto>>(accounts);
        }

        private async Task ValidateCustomerExistsAsync(long customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            _customerServiceValidator.ValidateCustomerExists(customerId, customer);
        }

        private async Task<Account> GetAccountAsync(long accountId)
        {
            var account = await _accountRepository.GetAccountByAccountIdAsync(accountId);
            _accountServiceValidator.ValidateAccountExists(accountId, account);

            return account;
        }
    }
}
