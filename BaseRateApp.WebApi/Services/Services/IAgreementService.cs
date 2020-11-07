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
    }
}
