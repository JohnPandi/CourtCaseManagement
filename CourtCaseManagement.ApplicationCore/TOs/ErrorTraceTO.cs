using Newtonsoft.Json;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ErrorTraceTO
    {
        [JsonProperty("traceId")]
        public virtual string TraceId { get; set; }

        [JsonProperty("message")]
        public virtual string Message { get; set; }
    }
}