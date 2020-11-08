using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BaseRateApp.Services.Integrations.Implementations
{
    public class VilibidClient : IVilibidClient
    {
        private HttpClient _httpClient;
        private readonly Uri _baseUri;

        public VilibidClient(string baseUrl)
        {
            _baseUri = new Uri(baseUrl);
        }

        public async Task<decimal> GetBaseRateValue(string baseRateCode)
        {
            var builder = new UriBuilder(_baseUri);
            builder.Path = "VilibidVilibor/VilibidVilibor.asmx/getLatestVilibRate";
            builder.Query = $"RateType={baseRateCode}";

            var response = await GetClient().GetAsync(builder.Uri);

            var serializer = new XmlSerializer(typeof(decimal), _baseUri.ToString());

            return (decimal)serializer.Deserialize(await response.Content.ReadAsStreamAsync());
        }

        private HttpClient GetClient()
        {
            if (_httpClient != null)
                return _httpClient;

            return new HttpClient(new HttpClientHandler())
            {
                Timeout = TimeSpan.FromMinutes(1)
            };
        }
    }
}
