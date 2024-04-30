﻿using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Interfaces;
using QuickBank.Entities.Enums;
using QuickBank.Models;

namespace QuickBank.API.Controllers
{
    [ApiController]
    [Route("transactions")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Route("deposit")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Deposit(DepositRequest depositRequest)
        {
            var referenceNumber = await _transactionService.Deposit(depositRequest);
            return Ok(referenceNumber);
        }

        [HttpPost]
        [Route("withdrawal")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Withdrawal(WithdrawalRequest withdrawalRequest)
        {
            var referenceNumber = await _transactionService.Withdrawal(withdrawalRequest);
            return Ok(referenceNumber);
        }

        [HttpPost]
        [Route("transfer-fund")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> TransferFund(FundTransferRequest fundTransferRequest)
        {
            var referenceNumber = await _transactionService.TransferFundAsync(fundTransferRequest);
            return Ok(referenceNumber);
        }

        [HttpGet]
        [Route("~/accounts/{accountId}/transactions")]
        [ProducesResponseType(typeof(List<TransactionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TransactionDto>>> GetTransactions(
            long accountId, TransactionRange transactionRange, DateTime? startDate, DateTime? endDate)
        {
            var transactions = await _transactionService.GetTransactionsAsync(
                accountId, transactionRange, startDate, endDate);
            return Ok(transactions);
        }

        [HttpGet]
        [Route("{transactionId}")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionDto>> GetTransactionById(long transactionId)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(transactionId);
            return Ok(transaction);
        }

        [HttpGet]
        [Route("reference-number/{referenceNumber}")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionDto>> GetTransactionByReferenceNumber(string referenceNumber)
        {
            var transaction = await _transactionService.GetTransactionByReferenceNumberAsync(referenceNumber);
            return Ok(transaction);
        }
    }
}
