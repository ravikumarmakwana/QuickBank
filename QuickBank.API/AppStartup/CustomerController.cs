using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickBank.Business.Interfaces;
using QuickBank.Models;

namespace QuickBank.API.AppStartup
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("{customerId}/addresses")]
        public async Task<ActionResult> AddNewAddressAsync(long customerId, AddAddressRequest addAddressRequest)
        {
            await _customerService.AddAddressAsync(customerId, addAddressRequest);
            return NoContent();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult> UpdateAddressAsync(long customerId, long addressId, UpdateAddressRequest updateAddressRequest)
        {
            await _customerService.UpdateAddressAsync(customerId, addressId, updateAddressRequest);
            return NoContent();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{customerId}/addresses/{addressId}")]
        public async Task<ActionResult> RemoveAddressAsync(long customerId, long addressId)
        {
            await _customerService.DeleteAddressAsync(customerId, addressId);
            return NoContent();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPost("{customerId}/PII")]
        public async Task<ActionResult> AddCustomerPIIAsync(long customerId, CustomerPIIModel customerPII)
        {
            await _customerService.AddCustomerPIIAsync(customerId, customerPII);
            return NoContent();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{customerId}/update-email")]
        public async Task<ActionResult> UpdateEmailAsync(long customerId, UpdateEmailRequest updateEmailRequest)
        {
            await _customerService.UpdateEmailAsync(customerId, updateEmailRequest);
            return NoContent();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{customerId}/update-mobile-number")]
        public async Task<ActionResult> UpdateEmailAsync(long customerId, UpdatePhoneNumberRequest updatePhoneNumberRequest)
        {
            await _customerService.UpdatePhoneNumberAsync(customerId, updatePhoneNumberRequest);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{customerId}")]
        public async Task<ActionResult> RemoveCustomerByIdAsync(long customerId)
        {
            await _customerService.RemoveCustomerByIdAsync(customerId);
            return NoContent();
        }

        [Authorize(Roles = "Admin,Role")]
        [HttpGet("{customerId}")]
        public async Task<ActionResult> GetCustomerByIdAsync(long customerId)
        {
            return Ok(await _customerService.GetCustomerByIdAsync(customerId));
        }
    }
}
