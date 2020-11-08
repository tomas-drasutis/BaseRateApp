using BaseRateApp.Models.Request;
using BaseRateApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BaseRateApp.Services
{
    public interface IAgreementService
    {
        Task<AgreementResponse> GetById(Guid id);
        Task<IEnumerable<AgreementResponse>> GetAll();
        Task<AgreementResponse> Create(AgreementRequest model);
        Task<AgreementResponse> Update(Guid id, AgreementRequest model);
        Task<IEnumerable<AgreementResponse>> GetByClientId(Guid clientId);
        Task<AgreementResponse> Delete(Guid id);

        Task<AgreementInterestResponse> AgreementInterestChange(AgreementInterestRequest model);
    }
}
