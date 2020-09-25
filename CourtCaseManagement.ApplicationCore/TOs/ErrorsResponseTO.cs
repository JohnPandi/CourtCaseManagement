using Newtonsoft.Json;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ErrorsResponseTO
    {
        [JsonProperty("errors")]
        public virtual List<ErrorsTO> Errors { get; set; } = new List<ErrorsTO>();
    }
}