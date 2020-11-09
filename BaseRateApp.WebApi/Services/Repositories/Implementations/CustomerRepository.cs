using AutoMapper;
using BaseRateApp.Models.Response;
using BaseRateApp.Persistance;
using BaseRateApp.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseRateApp.Services.Repositories.Implementations
{
    public class CustomerRepository : BaseRepository<CustomerResponse, CustomerRequest, Customer>, ICustomerRepository
    {
        public CustomerRepository(IDatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
        {
        }

        protected override Customer UpdateEntity(Customer entity, CustomerRequest model)
        {
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;

            return entity;
        }
    }
}
