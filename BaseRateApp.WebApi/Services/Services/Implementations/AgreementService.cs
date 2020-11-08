using BaseRateApp.Models.Response;
using BaseRateApp.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.Services.Implementations
{
    public class AgreementService : IAgreementService
    {
        private readonly IAgreementRepository _agreementRepository;

        public AgreementService(IAgreementRepository agreementRepository)
        {
            _agreementRepository = agreementRepository;
        }

        public Task<AgreementResponse> GetById(Guid id) => _agreementRepository.GetById(id);

        public Task<IEnumerable<AgreementResponse>> GetAll() => _agreementRepository.GetAll();

        public Task<AgreementResponse> Create(AgreementRequest model) => _agreementRepository.Create(model);

        public Task<AgreementResponse> Update(Guid id, AgreementRequest model) => _agreementRepository.Update(id, model);

        public Task<IEnumerable<AgreementResponse>> GetByClientId(Guid clientId) => _agreementRepository.GetByClientId(clientId);
    }
}
