using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Models.Request
{
    public class AgreementInterestRequest
    {
        [JsonRequired]
        public Guid CustomerId { get; set; }
        [JsonRequired]
        public Guid AgreementId { get; set; }
        [JsonRequired]
        public string NewBaseRateCode { get; set; }
    }
}
