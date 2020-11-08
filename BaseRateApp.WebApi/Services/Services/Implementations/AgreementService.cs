using BaseRateApp.Models.Request;
using BaseRateApp.Models.Response;
using BaseRateApp.Services.Integrations;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IVilibidClient _vilibidClient;

        public AgreementService(IAgreementRepository agreementRepository, ICustomerRepository customerRepository, IVilibidClient vilibidClient)
        {
            _agreementRepository = agreementRepository;
            _customerRepository = customerRepository;
            _vilibidClient = vilibidClient;
        }

        public Task<AgreementResponse> GetById(Guid id) => _agreementRepository.GetById(id);

        public Task<IEnumerable<AgreementResponse>> GetAll() => _agreementRepository.GetAll();

        public Task<AgreementResponse> Create(AgreementRequest model) => _agreementRepository.Create(model);

        public Task<AgreementResponse> Update(Guid id, AgreementRequest model) => _agreementRepository.Update(id, model);

        public Task<AgreementResponse> Delete(Guid id) => _agreementRepository.Delete(id);

        public Task<IEnumerable<AgreementResponse>> GetByClientId(Guid clientId) => _agreementRepository.GetByClientId(clientId);

        public async Task<AgreementInterestResponse> AgreementInterestChange(AgreementInterestRequest model)
        {
            var customer = await _customerRepository.GetById(model.CustomerId);

            var agreement = await GetById(model.AgreementId);

            var currentInterestRate = await _vilibidClient.GetBaseRateValue(agreement.BaseRateCode) + agreement.Margin;

            var newInterestRate = await _vilibidClient.GetBaseRateValue(model.NewBaseRateCode) + agreement.Margin;

            return new AgreementInterestResponse()
            {
                Customer = customer,
                Agreement = agreement,
                CurrentInterestRate = currentInterestRate,
                NewInterestRate = newInterestRate,
                InterestDifference = currentInterestRate - newInterestRate
            };
        }
    }
}
