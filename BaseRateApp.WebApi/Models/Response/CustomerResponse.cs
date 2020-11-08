﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Models.Response
{
    public class CustomerResponse : CustomerRequest, IResponseModel<Guid>
    {
        public Guid Id { get; set; }
    }
}
