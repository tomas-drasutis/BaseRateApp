using BaseRateApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Services.Repositories
{
    public interface ICustomerRepository : IBaseRepository<CustomerResponse, CustomerRequest, Guid>
    {
    }
}
