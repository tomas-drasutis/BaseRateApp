using BaseRateApp.Models.Response;
using BaseRateApp.Services;
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
    public class AgreementController : ControllerBase
    {
        private readonly IAgreementService _agreementService;

        public AgreementController(IAgreementService agreementService)
        {
            _agreementService = agreementService;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        [ProducesResponseType(typeof(AgreementResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return new OkObjectResult(await _agreementService.GetById(id));
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AgreementResponse>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll()
        {
            return new OkObjectResult(await _agreementService.GetAll());
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ProducesResponseType(typeof(AgreementResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] AgreementRequest agreementRequest)
        {
            return new OkObjectResult(await _agreementService.Create(agreementRequest));
        }

        [Microsoft.AspNetCore.Mvc.HttpPut]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        [ProducesResponseType(typeof(AgreementResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] AgreementRequest agreementRequest)
        {
            return new OkObjectResult(await _agreementService.Update(id, agreementRequest));
        }
    }
}
