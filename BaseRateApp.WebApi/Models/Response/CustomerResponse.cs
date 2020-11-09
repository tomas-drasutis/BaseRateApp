using System;

namespace BaseRateApp.Models.Response
{
    public class CustomerResponse : CustomerRequest, IResponseModel<Guid>
    {
        public Guid Id { get; set; }
    }
}
