using BaseRateApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.Services.CustomerService.Implementations
{
    public class CustomerService : ICustomerService
    {
        public Task<CustomerResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerResponse>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
