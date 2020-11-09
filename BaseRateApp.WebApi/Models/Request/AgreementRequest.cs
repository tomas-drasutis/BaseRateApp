using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace BaseRateApp.Models.Response
{
    public class AgreementRequest
    {
        [JsonRequired]
        public decimal Amount { get; set; }

        [JsonRequired]
        public string BaseRateCode { get; set; } //Should be enum

        [JsonRequired, Range(0.0, 99.99)]
        public decimal Margin { get; set; }

        [JsonRequired]
        public int AgreementDuration { get; set; }

        [JsonRequired]
        public Guid CustomerId { get; set; }
    }
}
