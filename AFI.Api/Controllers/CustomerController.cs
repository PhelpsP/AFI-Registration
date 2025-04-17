using AFI.Api.Models;
using AFI.Api.Resources;
using AFI.Application;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AFI.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRegistrationService _service;

        public CustomerController(ICustomerRegistrationService service) 
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CustomerRegistrationRequestDto? inputDto)
        {
            if (inputDto == null)
            {
                return BadRequest("Invalid input");
            }

            try
            {
                var dto = new CustomerDetailsDto
                {
                    PolicyholderFirstName = inputDto.PolicyholderFirstName,
                    PolicyholderSurname = inputDto.PolicyholderSurname,
                    PolicyReferenceNumber = inputDto.PolicyReferenceNumber,
                    PolicyholderDOB = inputDto.PolicyholderDOB,
                    PolicyholderEmail = inputDto.PolicyholderEmail,
                };

                var result = await _service.RegisterCustomerAsync(dto);
                var response = new RegistrationResponse { CustomerId = result };

                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "An unexpected error occurred");
            }
        }
    }
}
