using Newtonsoft.Json;

namespace process.Application.Model
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } 
        public string? Source { get; set; } 
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}
