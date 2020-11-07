using Newtonsoft.Json;

namespace Models
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
