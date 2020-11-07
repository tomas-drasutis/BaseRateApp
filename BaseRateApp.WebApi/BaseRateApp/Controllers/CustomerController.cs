using BaseRateApp.Models.Response;
using BaseRateApp.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseRateApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController
    {

        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return new OkObjectResult(await _customerService.GetById(id));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerResponse>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _customerService.GetAll());
        }
    }
}
