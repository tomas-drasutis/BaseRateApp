using AutoMapper;
using BaseRateApp.Models.Response;
using BaseRateApp.Persistance.Entities;

namespace BaseRateApp.Services.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<AgreementRequest, Agreement>();
            CreateMap<Agreement, AgreementResponse>();

            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
    }
}
