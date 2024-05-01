using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Interfaces;
using QuickBank.Core.Constants;
using QuickBank.Models;

namespace QuickBank.API.Controllers
{
    [Route("customers")]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = Constants.CustomerAccess)]
        [HttpPost("{customerId}/addresses")]
        public async Task<ActionResult> AddNewAddressAsync(long customerId, AddAddressRequest addAddressRequest)
        {
            await _customerService.AddAddressAsync(customerId, addAddressRequest);
            return NoContent();
        }

        [Authorize(Roles = Constants.CustomerAccess)]
        [HttpPut("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult> UpdateAddressAsync(long customerId, long addressId, UpdateAddressRequest updateAddressRequest)
        {
            await _customerService.UpdateAddressAsync(customerId, addressId, updateAddressRequest);
            return NoContent();
        }

        [Authorize(Roles = Constants.CustomerAccess)]
        [HttpDelete("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult> RemoveAddressAsync(long customerId, long addressId)
        {
            await _customerService.DeleteAddressAsync(customerId, addressId);
            return NoContent();
        }

        [Authorize(Roles = Constants.CustomerAccess)]
        [HttpPost("{customerId}/PII")]
        public async Task<ActionResult> AddCustomerPIIAsync(long customerId, CustomerPIIModel customerPII)
        {
            await _customerService.AddCustomerPIIAsync(customerId, customerPII);
            return NoContent();
        }

        [Authorize(Roles = Constants.CustomerAccess)]
        [HttpPut("{customerId}/update-email")]
        public async Task<ActionResult> UpdateEmailAsync(long customerId, UpdateEmailRequest updateEmailRequest)
        {
            await _customerService.UpdateEmailAsync(customerId, updateEmailRequest);
            return NoContent();
        }

        [Authorize(Roles = Constants.CustomerAccess)]
        [HttpPut("{customerId}/update-mobile-number")]
        public async Task<ActionResult> UpdateEmailAsync(long customerId, UpdatePhoneNumberRequest updatePhoneNumberRequest)
        {
            await _customerService.UpdatePhoneNumberAsync(customerId, updatePhoneNumberRequest);
            return NoContent();
        }

        [Authorize(Roles = Constants.AdminAccess)]
        [HttpDelete("{customerId}")]
        public async Task<ActionResult> RemoveCustomerByIdAsync(long customerId)
        {
            await _customerService.RemoveCustomerByIdAsync(customerId);
            return NoContent();
        }

        [Authorize(Roles = Constants.CustomerAccess)]
        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetCustomerByIdAsync(long customerId)
        {
            return Ok(await _customerService.GetCustomerByIdAsync(customerId));
        }
    }
}
