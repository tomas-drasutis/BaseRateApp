using BaseRateApp.Models.Response;
using BaseRateApp.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseRateApp.Services.CustomerService.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<CustomerResponse> Create(CustomerRequest model) => _customerRepository.Create(model);

        public Task<IEnumerable<CustomerResponse>> GetAll() => _customerRepository.GetAll();

        public Task<CustomerResponse> GetById(Guid id) => _customerRepository.GetById(id);

        public Task<CustomerResponse> Update(Guid id, CustomerRequest model) => _customerRepository.Update(id, model);

        public Task<CustomerResponse> Delete(Guid id) => _customerRepository.Delete(id);

    }
}
