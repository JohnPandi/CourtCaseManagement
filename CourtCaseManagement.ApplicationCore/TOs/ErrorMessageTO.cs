using Newtonsoft.Json;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ErrorMessageTO
    {
        [JsonProperty("message")]
        public virtual string Message { get; set; }
    }
}