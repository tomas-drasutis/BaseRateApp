using AutoMapper;
using BaseRateApp.Models.Response;
using BaseRateApp.Persistance;
using BaseRateApp.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseRateApp.Services.Repositories.Implementations
{
    public class AgreementRepository : BaseRepository<AgreementResponse, AgreementRequest, Agreement>, IAgreementRepository
    {
        public AgreementRepository(IDatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
        {
        }

        public async Task<IEnumerable<AgreementResponse>> GetByClientId(Guid clientId)
        {
            return (await dbContext.Agreements
                .Where(e => e.CustomerId == clientId)
                .ToListAsync())
                .Select(ConvertToResponseModel); ;
        }

        protected override Agreement UpdateEntity(Agreement entity, AgreementRequest model)
        {
            entity.AgreementDuration = model.AgreementDuration;
            entity.Amount = model.Amount;
            entity.BaseRateCode = model.BaseRateCode;

            return entity;
        }
    }
}
