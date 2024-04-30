using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Interfaces;
using QuickBank.Models;

namespace QuickBank.API.Controllers
{
    [ApiController]
    [Route("fixed-deposits")]
    public class FixedDepositController : ControllerBase
    {
        private readonly IFixedDepositService _fixedDepositService;

        public FixedDepositController(IFixedDepositService fixedDepositService)
        {
            _fixedDepositService = fixedDepositService;
        }

        [HttpPost]
        [Route("open-fd")]
        [ProducesResponseType(typeof(FixedDepositDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<FixedDepositDto>> AddFixedDeposit(FixedDepositRequest fixedDepositRequest)
        {
            var fixedDeposit = await _fixedDepositService.AddFixedDeposit(fixedDepositRequest);
            return Ok(fixedDeposit);
        }

        [HttpPut]
        [Route("close-fd/{fixedDepositId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> CloseFixedDeposit(long fixedDepositId)
        {
            await _fixedDepositService.CloseFixedDepositById(fixedDepositId);
            return NoContent();
        }

        [HttpGet]
        [Route("{fixedDepositId}")]
        [ProducesResponseType(typeof(FixedDepositDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<FixedDepositDto>> GetFixedDepositById(long fixedDepositId)
        {
            var fixedDeposit = await _fixedDepositService.GetFixedDepositById(fixedDepositId);
            return Ok(fixedDeposit);
        }

        [HttpGet]
        [Route("~/accounts/{accountId}/fixed-deposits")]
        [ProducesResponseType(typeof(List<FixedDepositDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FixedDepositDto>>> GetFixedDepositForAccount(long accountId)
        {
            var fixedDeposits = await _fixedDepositService.GetAllFixedDepositForAccount(accountId);
            return Ok(fixedDeposits);
        }

        [HttpGet]
        [Route("~/customers/{customerId}/fixed-deposits")]
        [ProducesResponseType(typeof(List<FixedDepositDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<FixedDepositDto>>> GetFixedDepositForCustomer(long customerId)
        {
            var fixedDeposits = await _fixedDepositService.GetAllFixedDepositForCustomer(customerId);
            return Ok(fixedDeposits);
        }
    }
}
