using BaseRateApp.Models.Request;
using BaseRateApp.Models.Response;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BaseRateApp.IntegrationTests
{
    [TestFixture]
    public class Tests
    {
        private APIWebApplicationFactory _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new APIWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task GetCustomers_ShouldReturnOk()
        {
            var result = await _client.GetAsync("/customer");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task PostExistingCustomerAndAgreement_ShouldReturnOk()
        {
            var customer = await CreateUser();
            var agreement = await CreateAgreement(customer);

            var agreementInterest = new AgreementInterestRequest()
            {
                CustomerId = agreement.CustomerId,
                AgreementId = agreement.Id,
                NewBaseRateCode = "VILIBOR6m"
            };

            StringContent agreementInterestData = new StringContent(JsonConvert.SerializeObject(agreementInterest), Encoding.UTF8, "application/json");
            var result = await (await _client.PostAsync("/agreement/interest", agreementInterestData)).Content.ReadAsAsync<AgreementInterestResponse>();

            Assert.AreEqual(customer.Id, result.Customer.Id);
            Assert.AreEqual(agreement.Id, result.Agreement.Id);
            Assert.NotNull(result.CurrentInterestRate);
            Assert.NotNull(result.NewInterestRate);
            Assert.NotNull(result.InterestDifference);

            await _client.DeleteAsync($"/customer/{customer.Id}");
            await _client.DeleteAsync($"/agreement/{agreement.Id}");
        }

        [Test]
        public async Task PostNonExistingCustomer_ShouldReturnNotFound()
        {
            var testGuid = Guid.NewGuid();

            var agreementInterest = new AgreementInterestRequest()
            {
                CustomerId = testGuid,
                AgreementId = testGuid,
                NewBaseRateCode = "VILIBOR6m"
            };

            StringContent agreementInterestData = new StringContent(JsonConvert.SerializeObject(agreementInterest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/agreement/interest", agreementInterestData);

            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Test]
        public async Task PostNonExistingBaseRateCode_ShouldReturnBadRequest()
        {
            var customer = await CreateUser();
            var agreement = await CreateAgreement(customer);

            var agreementInterest = new AgreementInterestRequest()
            {
                CustomerId = customer.Id,
                AgreementId = agreement.Id,
                NewBaseRateCode = "test"
            };

            StringContent agreementInterestData = new StringContent(JsonConvert.SerializeObject(agreementInterest), Encoding.UTF8, "application/json");
            var result = await _client.PostAsync("/agreement/interest", agreementInterestData);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);

            await _client.DeleteAsync($"/customer/{customer.Id}");
            await _client.DeleteAsync($"/agreement/{agreement.Id}");
        }

        private async Task<CustomerResponse> CreateUser()
        {
            var customerRequest = new CustomerRequest()
            {
                FirstName = "TestName",
                LastName = "TestLastName",
                PersonalId = "67812203006"
            };

            StringContent customerData = new StringContent(JsonConvert.SerializeObject(customerRequest), Encoding.UTF8, "application/json");
            var customer = await (await _client.PostAsync("/customer", customerData)).Content.ReadAsAsync<CustomerResponse>();

            Assert.AreEqual(customerRequest.FirstName, customer.FirstName);
            Assert.AreEqual(customerRequest.LastName, customer.LastName);
            Assert.AreEqual(customerRequest.PersonalId, customer.PersonalId);
            Assert.IsNotNull(customer.Id);

            return customer;
        }

        private async Task<AgreementResponse> CreateAgreement(CustomerResponse customer)
        {
            var agreementRequest = new AgreementRequest()
            {
                Amount = 1200,
                BaseRateCode = "VILIBOR1y",
                Margin = (decimal)1.6,
                AgreementDuration = 60,
                CustomerId = customer.Id,
            };

            StringContent agreementData = new StringContent(JsonConvert.SerializeObject(agreementRequest), Encoding.UTF8, "application/json");
            var agreement = await (await _client.PostAsync("/agreement", agreementData)).Content.ReadAsAsync<AgreementResponse>();

            Assert.AreEqual(agreementRequest.Amount, agreement.Amount);
            Assert.AreEqual(agreementRequest.BaseRateCode, agreement.BaseRateCode);
            Assert.AreEqual(agreementRequest.Margin, agreement.Margin);
            Assert.AreEqual(agreementRequest.AgreementDuration, agreement.AgreementDuration);
            Assert.AreEqual(agreementRequest.CustomerId, agreement.CustomerId);

            return agreement;
        }
    }
}