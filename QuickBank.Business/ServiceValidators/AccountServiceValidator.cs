using QuickBank.Business.Exceptions;
using QuickBank.Data.Interfaces;
using QuickBank.Entities;
using QuickBank.Entities.Enums;

namespace QuickBank.Business.ServiceValidators
{
    public class AccountServiceValidator : IAccountServiceValidator
    {
        private readonly IBankBranchRepository _bankBranchRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;

        public AccountServiceValidator(
            IBankBranchRepository bankBranchRepository, IAccountTypeRepository accountTypeRepository)
        {
            _bankBranchRepository = bankBranchRepository;
            _accountTypeRepository = accountTypeRepository;
        }

        public async Task DoesIFSCExists(string IFSC)
        {
            var bankCode = IFSC.Substring(0, 4);
            var reservedCharacter = IFSC.Substring(4, 1);
            var branchCode = IFSC.Substring(5);

            var isExists = await _bankBranchRepository.DoesBankBranchExistsAsync(bankCode, reservedCharacter, branchCode);

            if (!isExists)
            {
                throw new InvalidOperationException($"Invalid IFSC: {IFSC}");
            }
        }

        public async Task DoesAccountTypeExists(long accountTypeId)
        {
            var accountType = await _accountTypeRepository.GetAccountTypeByIdAsync(accountTypeId);

            if (accountType == null)
            {
                throw new InvalidOperationException($"Invalid AccountTypeId : {accountTypeId}");
            }
        }

        public void ValidateAccountExists(long accountId, Account account)
        {
            if (account == null)
            {
                throw new AccountNotFoundException(
                    $"Account doesn't exists for given AccountId: {accountId}"
                );
            }

            if (account.AccountStatus != AccountStatus.Active)
            {
                throw new InvalidOperationException(
                    $"Account Status: {account.AccountStatus} for given AccountId: {accountId}"
                );
            }
        }
    }
}
