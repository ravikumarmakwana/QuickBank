using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Interfaces;
using QuickBank.Core.Constants;
using QuickBank.Models;

namespace QuickBank.API.Controllers
{
    [Route("accounts")]
    public class AccountController : BaseController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Authorize(Roles = Constants.CustomerAccess)]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<AccountDto>> CreateNewAccount(AccountCreationRequest accountCreationRequest)
        {
            var account = await _accountService.CreateAccountAsync(accountCreationRequest);
            return Ok(account);
        }

        [HttpGet]
        [Route("{accountId}")]
        [Authorize(Roles = Constants.CustomerAccess)]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<AccountDto>> GetAccountByAccountId(long accountId)
        {
            var account = await _accountService.GetAccountByAccountIdAsync(accountId);
            return Ok(account);
        }

        [HttpGet]
        [Route("~/customers/{customerId}/accounts")]
        [Authorize(Roles = Constants.CustomerAccess)]
        [ProducesResponseType(typeof(List<AccountDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AccountDto>>> GetAccountsByCustomerId(long customerId)
        {
            var accounts = await _accountService.GetAccountsByCustomerIdAsync(customerId);
            return Ok(accounts);
        }

        [HttpPut]
        [Route("close-account/{accountId}")]
        [Authorize(Roles = Constants.AdminAccess)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CloseAccountByAccountId(long accountId)
        {
            await _accountService.CloseAccountByAccountIdAsync(accountId);
            return NoContent();
        }

        [HttpPost]
        [Route("add-quarterly-interest")]
        public async Task<ActionResult> AddQuarterlyInterest()
        {
            await _accountService.DepositQuarterlyInterest(GetCurrentQuarter());
            return Ok();
        }

        private int GetCurrentQuarter()
        {
            var currentMonth = DateTime.Now.Month;

            if (currentMonth <= 3)
            {
                return 1;
            }
            else if (currentMonth >= 4 || currentMonth <= 6)
            {
                return 2;
            }
            else if (currentMonth >= 7 || currentMonth <= 9)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
    }
}
