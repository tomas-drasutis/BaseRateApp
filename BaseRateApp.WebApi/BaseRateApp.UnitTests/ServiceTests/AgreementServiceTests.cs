using BaseRateApp.Models.Request;
using BaseRateApp.Models.Response;
using BaseRateApp.Services;
using BaseRateApp.Services.Implementations;
using BaseRateApp.Services.Integrations;
using BaseRateApp.Services.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseRateApp.UnitTests
{
    public class AgreementServiceTests
    {
        private readonly IAgreementService _service;
        private readonly Mock<IAgreementRepository> _agreementRepository;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<IVilibidClient> _vilibidClient;

        public AgreementServiceTests()
        {
            _agreementRepository = new Mock<IAgreementRepository>();
            _customerRepository = new Mock<ICustomerRepository>();
            _vilibidClient = new Mock<IVilibidClient>();
            _service = new AgreementService(_agreementRepository.Object, _customerRepository.Object, _vilibidClient.Object);
        }

        [Test]
        public async Task GetAgreementById_ShouldSucceed()
        {
            var agreements = GetAgreements();
            var expectedAgreement = agreements.First();

            _agreementRepository
                .Setup(r => r.GetById(expectedAgreement.Id))
                .Returns(Task.FromResult(expectedAgreement));

            var actualAgreement = await _service.GetById(expectedAgreement.Id);

            Assert.AreEqual(expectedAgreement.Amount, actualAgreement.Amount);
        }

        [Test]
        public async Task GetAgreementInterest_ShouldSucceed()
        {
            var testBaseRateCode = "VILIBOR6m";

            var agreements = GetAgreements();
            var expectedAgreement = agreements.First();

            var customers = GetCustomers();
            var expectedCustomer = customers.First();

            var newBaseRate = (decimal)1.4;
            var oldBaseRate = (decimal)1;

            _agreementRepository
                .Setup(r => r.GetById(expectedAgreement.Id))
                .Returns(Task.FromResult(expectedAgreement));

            _customerRepository
                .Setup(r => r.GetById(expectedCustomer.Id))
                .Returns(Task.FromResult(expectedCustomer));

            _vilibidClient
                .Setup(r => r.GetBaseRateValue("VILIBOR6m"))
                .Returns(Task.FromResult(newBaseRate));

            _vilibidClient
                .Setup(r => r.GetBaseRateValue(expectedAgreement.BaseRateCode))
                .Returns(Task.FromResult(oldBaseRate));

            var agreementInterest = await _service.AgreementInterestChange(new AgreementInterestRequest() 
            { 
                CustomerId = expectedAgreement.CustomerId,
                AgreementId = expectedAgreement.Id,
                NewBaseRateCode = testBaseRateCode
            });

            Assert.AreEqual(expectedAgreement.Margin + oldBaseRate, agreementInterest.CurrentInterestRate);
            Assert.AreEqual(expectedAgreement.Margin + newBaseRate, agreementInterest.NewInterestRate);
            Assert.AreEqual(oldBaseRate - newBaseRate, agreementInterest.InterestDifference);
        }

        public IEnumerable<AgreementResponse> GetAgreements()
        {
            return new[]
            {
                new AgreementResponse
                {
                    Id= Guid.Parse("dacb5598-0199-4060-1bd2-08d883e552be"),
                    Amount= 12000,
                    BaseRateCode= "VILIBOR3m",
                    Margin= (decimal)1.6,
                    AgreementDuration= 60,
                    CustomerId= Guid.Parse("48d3ac45-906d-4953-e88e-08d883e3103a")

                },
                new AgreementResponse
                {
                    Id= Guid.Parse("30445fce-2e24-4d40-1bd3-08d883e552be"),
                    Amount= 8000,
                    BaseRateCode= "VILIBOR1y",
                    Margin= (decimal)2.2,
                    AgreementDuration= 36,
                    CustomerId= Guid.Parse("853c6895-f920-4cd4-551e-08d883e3e9e9")
                },
                new AgreementResponse
                {
                    Id= Guid.Parse("f51a6dc7-7eee-4101-1bd4-08d883e552be"),
                    Amount= 1000,
                    BaseRateCode= "VILIBOR6m",
                    Margin= (decimal)1.85,
                    AgreementDuration= 24,
                    CustomerId= Guid.Parse("853c6895-f920-4cd4-551e-08d883e3e9e9")
                },
            };
        }
        public IEnumerable<CustomerResponse> GetCustomers()
        {
            return new[]
            {
                new CustomerResponse
                {
                    Id= Guid.Parse("48d3ac45-906d-4953-e88e-08d883e3103a"),
                    FirstName = "Goras",
                    LastName = "Trusevicius",
                    PersonalId = "67812203006"
                },
                new CustomerResponse
                {
                    Id= Guid.Parse("853c6895-f920-4cd4-551e-08d883e3e9e9"),
                    FirstName = "Dange",
                    LastName = "Kulkavičiutė",
                    PersonalId = "78706151287"
                }
            };
        }
    }
}