using Newtonsoft.Json;

namespace BaseRateApp.Models.Response
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
