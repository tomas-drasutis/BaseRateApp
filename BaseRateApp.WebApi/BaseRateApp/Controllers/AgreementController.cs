﻿using BaseRateApp.Models.Enums;
using BaseRateApp.Models.Request;
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
            if (!BaseRateCodeExists(agreementRequest.BaseRateCode))
            {
                return new BadRequestResult();
            }

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

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        [Microsoft.AspNetCore.Mvc.Route("{id}")]
        [ProducesResponseType(typeof(AgreementResponse), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            return new OkObjectResult(await _agreementService.Delete(id));
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.Route("interest")]
        [ProducesResponseType(typeof(AgreementInterestResponse), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SubmitAgreement([FromBody] AgreementInterestRequest agreementInterestRequest)
        {
            if (!BaseRateCodeExists(agreementInterestRequest.NewBaseRateCode))
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(await _agreementService.AgreementInterestChange(agreementInterestRequest));
        }

        private bool BaseRateCodeExists(string baseRateCode)
        {
            return Enum.GetNames(typeof(BaseRateCode)).Contains(baseRateCode);
        }
    }
}
