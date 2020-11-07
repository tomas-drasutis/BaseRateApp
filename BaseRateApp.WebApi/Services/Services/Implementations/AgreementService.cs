using BaseRateApp.Models.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.Services.Implementations
{
    public class AgreementService : IAgreementService
    {
        public Task<AgreementResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AgreementResponse>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
