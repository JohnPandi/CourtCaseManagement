using Newtonsoft.Json;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ErrorsTO
    {
        [JsonProperty("field")]
        public virtual string Field { get; set; }

        [JsonProperty("validation")]
        public virtual string Validation { get; set; }
    }
}