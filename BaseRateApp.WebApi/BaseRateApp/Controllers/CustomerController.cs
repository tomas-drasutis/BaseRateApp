using BaseRateApp.Models.Response;
using BaseRateApp.Services;
using BaseRateApp.Services.CustomerService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseRateApp.WebApi.Controllers
{
    [ProducesResponseType(typeof(ErrorResponse), 500)]
    [ApiController]
    [Route("[controller]")]
    public class CustomerController
    {
        private readonly ICustomerService _customerService;
        private readonly IAgreementService _agreementService;

        public CustomerController(ICustomerService customerService, IAgreementService agreementService)
        {
            _customerService = customerService;
            _agreementService = agreementService;
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

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ProducesResponseType(typeof(CustomerResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] CustomerRequest customerRequest)
        {
            return new OkObjectResult(await _customerService.Create(customerRequest));
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        [ProducesResponseType(typeof(CustomerResponse), 200)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CustomerRequest customerRequest)
        {
            return new OkObjectResult(await _customerService.Update(id, customerRequest));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id}/agreement")]
        [ProducesResponseType(typeof(AgreementResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetClientAgreements([FromRoute] Guid id)
        {
            return new OkObjectResult(await _agreementService.GetByClientId(id));
        }
    }
}
