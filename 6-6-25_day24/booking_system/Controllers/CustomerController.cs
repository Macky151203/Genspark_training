using Microsoft.AspNetCore.Mvc;
using BookingSystem.Models;
using BookingSystem.Models.DTOs;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

namespace BookingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger<AuthenticationController> _logger;

        public CustomerController(ICustomerService customerService, ILogger<AuthenticationController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<ActionResult<Customer>> RegisterCustomer(CustomerDto customerDto)
        {
            var customer = await _customerService.RegisterCustomer(customerDto);
            if (customer == null)
            {
                _logger.LogError("Failed to register customer with email {Email}", customerDto.Email);
                return BadRequest("Failed to register customer.");
            }
            _logger.LogInformation("Customer {Email} registered successfully", customer.Email);
            return CreatedAtAction(nameof(GetCustomerByName), new { email = customer.Email }, customer);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Customer>> GetCustomerByName(string name)
        {
            try
            {
                var customer = await _customerService.GetCustomerByName(name);
                if (customer == null)
                {
                    _logger.LogWarning("Customer with name {name} not found", name);
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve customer with email {Email}", name);
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles="Customer")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve all customers");
                return BadRequest(ex.Message);
            }
        }
    }
}
