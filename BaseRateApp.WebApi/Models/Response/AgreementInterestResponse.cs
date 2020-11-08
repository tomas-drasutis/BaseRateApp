using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Models.Response
{
    public class AgreementInterestResponse
    {
        public CustomerResponse Customer { get; set; }
        public AgreementResponse Agreement { get; set; }
        public decimal CurrentInterestRate { get; set; }
        public decimal NewInterestRate { get; set; }
        public decimal InterestDifference { get; set; }
    }
}
