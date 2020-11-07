using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Models.Response
{
    public class CustomerResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
    }
}
