using CustomersApi.Interfaces;
using CustomersApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomer _customerService;
        public CustomerController(ICustomer customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task Post([FromBody] Customer customer)
        {
            await _customerService.AddCustomer(customer);
        }
    }
}