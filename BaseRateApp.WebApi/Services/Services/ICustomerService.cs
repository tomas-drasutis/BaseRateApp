using BaseRateApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseRateApp.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<CustomerResponse> GetById(Guid id);
        Task<IEnumerable<CustomerResponse>> GetAll();
        Task<CustomerResponse> Create(CustomerRequest model);
        Task<CustomerResponse> Update(Guid id, CustomerRequest model);
    }
}
