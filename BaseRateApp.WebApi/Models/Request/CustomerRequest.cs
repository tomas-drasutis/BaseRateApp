using Newtonsoft.Json;

namespace BaseRateApp.Models.Response
{
    public class CustomerRequest
    {
        [JsonRequired]
        public string FirstName { get; set; }

        [JsonRequired]
        public string LastName { get; set; }

        [JsonRequired]
        public string PersonalId { get; set; }
    }
}
