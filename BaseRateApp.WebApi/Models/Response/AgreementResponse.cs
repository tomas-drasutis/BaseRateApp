using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Models.Response
{
    public class AgreementResponse
    {
        public double Amount { get; set; }
        public string BaseRateCode { get; set; } //Should be enum
        public double Margin { get; set; }
        public int AgreementDuration { get; set; }
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
    }
}
