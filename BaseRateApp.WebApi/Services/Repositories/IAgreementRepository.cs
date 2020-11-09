using BaseRateApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.Services.Repositories
{
    public interface IAgreementRepository : IBaseRepository<AgreementResponse, AgreementRequest, Guid>
    {
        Task<IEnumerable<AgreementResponse>> GetByClientId(Guid clientId);
    }
}
