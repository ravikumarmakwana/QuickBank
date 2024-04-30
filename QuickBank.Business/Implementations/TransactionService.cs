using AutoMapper;
using QuickBank.Business.Exceptions;
using QuickBank.Business.Helpers;
using QuickBank.Business.Interfaces;
using QuickBank.Data.Interfaces;
using QuickBank.Entities.Enums;
using QuickBank.Entities;
using QuickBank.Models;

namespace QuickBank.Business.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public TransactionService(
            ITransactionRepository transactionRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<string> Deposit(DepositRequest depositRequest)
        {
            var transaction = new Transaction
            {
                AccountId = depositRequest.AccountId,
                TransactionType = TransactionType.Credit,
                Particulars = !string.IsNullOrEmpty(depositRequest.Particulars) ? depositRequest.Particulars : "Deposit",
                Amount = depositRequest.DepositAmount,
            };

            await PerformTransactionAsync(transaction);

            return Generator.EncryptTransactionId(transaction.TransactionId);
        }

        public async Task<string> Withdrawal(WithdrawalRequest withdrawalRequest)
        {
            var transaction = new Transaction
            {
                AccountId = withdrawalRequest.AccountId,
                TransactionType = TransactionType.Debit,
                Particulars = !string.IsNullOrEmpty(withdrawalRequest.Particulars) ? withdrawalRequest.Particulars : "Withdrawal",
                Amount = withdrawalRequest.WithdrawalAmount,
            };

            await PerformTransactionAsync(transaction);

            return Generator.EncryptTransactionId(transaction.TransactionId);
        }

        public async Task<string> TransferFundAsync(FundTransferRequest fundTransferRequest)
        {
            var depositeRequest = new DepositRequest
            {
                AccountId = fundTransferRequest.CreditAccountId,
                Particulars = fundTransferRequest.Particulars,
                DepositAmount = fundTransferRequest.TransactionAmount
            };
            await Deposit(depositeRequest);

            var withdrawalRequest = new WithdrawalRequest
            {
                AccountId = fundTransferRequest.DebitAccountId,
                Particulars = fundTransferRequest.Particulars,
                WithdrawalAmount = fundTransferRequest.TransactionAmount
            };
            var referenceNumber = await Withdrawal(withdrawalRequest);

            return referenceNumber;
        }

        public async Task<List<TransactionDto>> GetTransactionsAsync(
            long accountId, TransactionRange transactionRange, DateTime? startDate, DateTime? endDate)
        {
            DateTime localStartDate, localEndDate;

            await ValidateAccountExists(accountId);

            Utils.SetTransactionDates(transactionRange, out localStartDate, out localEndDate, in startDate, in endDate);

            var transactions = await _transactionRepository.GetTransactionsAsync(accountId, localStartDate, localEndDate);

            return _mapper.Map<List<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetTransactionByIdAsync(long transactionId)
        {
            var transaction = await _transactionRepository.GetTransactionByIdAsync(transactionId);
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> GetTransactionByReferenceNumberAsync(string referenceNumber)
        {
            var transactionId = Generator.DecryptTransactionId(referenceNumber);
            var transaction = await GetTransactionByIdAsync(transactionId);
            return transaction;
        }

        private async Task ValidateAccountExists(long accountId)
        {
            var account = await _accountRepository.GetAccountByAccountIdAsync(accountId);
            ValidateAccountExists(accountId, account);
        }

        private void ValidateAccount(long accountId, Account account, TransactionType transactionType, double transactionAmount)
        {
            ValidateAccountExists(accountId, account);

            if (transactionType == TransactionType.Debit && account.Balance < transactionAmount)
            {
                throw new InvalidTransactionException(
                    $"Account with AccountId: {accountId}, doesn't have enough balance to perform transaction"
                );
            }

            if (transactionType == TransactionType.Debit && account.AccountType.MinimumRequiredBalance > account.Balance - transactionAmount)
            {
                throw new InvalidTransactionException(
                    $"Maintain minimum required balance: {account.AccountType.MinimumRequiredBalance} for given account with AccountId: {accountId}"
                );
            }
        }

        private void ValidateAccountExists(long accountId, Account account)
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
                    $"Account is {account.AccountStatus} for given AccountId: {accountId}"
                );
            }
        }

        private async Task PerformTransactionAsync(Transaction transaction)
        {
            var account = await _accountRepository.GetAccountByAccountIdAsync(transaction.AccountId);
            ValidateAccount(transaction.AccountId, account, transaction.TransactionType, transaction.Amount);

            if (transaction.TransactionType == TransactionType.Credit)
            {
                account.Balance += transaction.Amount;
            }
            else
            {
                account.Balance -= transaction.Amount;
            }

            transaction.TotalBalance = account.Balance;
            transaction.TransactionDate = DateTime.Now;

            await _accountRepository.UpdateAccountAsync(account);
            await _transactionRepository.AddTransactionsAsync(transaction);
        }

        public async Task UpdateAccountStatus()
        {
            var accounts = await _accountRepository.GetAllAccounts();

            accounts = accounts
                .Where(a => a.AccountStatus != AccountStatus.Closed || a.AccountStatus != AccountStatus.Dormant)
                .ToList();

            foreach (var account in accounts)
            {
                var transaction = await _transactionRepository.GetLastTransactionAsync(account.AccountId);

                var numOfDays =
                    transaction == null ?
                    (DateTime.Now.Date - account.CreatedOn.Date).Days :
                    (DateTime.Now.Date - transaction.TransactionDate.Date).Days;

                bool isStatusUpdates = false;

                if (numOfDays >= account.AccountType.NumberOfDaysToInactive)
                {
                    account.AccountStatus = AccountStatus.Inactive;
                    isStatusUpdates = true;
                }

                if (numOfDays >= account.AccountType.NumberOfDaysToDormant)
                {
                    account.AccountStatus = AccountStatus.Dormant;
                    isStatusUpdates = true;
                }

                if (isStatusUpdates)
                {
                    await _accountRepository.UpdateAccountAsync(account);
                }
            }
        }
    }
}
