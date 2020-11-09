using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Models.Response
{
    public class AgreementResponse : AgreementRequest, IResponseModel<Guid>
    {
        public Guid Id { get; set; }
    }
}
