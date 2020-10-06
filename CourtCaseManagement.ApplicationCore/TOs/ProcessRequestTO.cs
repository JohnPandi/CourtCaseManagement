using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ProcessRequestTO
    {
        [JsonProperty("linkedProcessId")]
        public Guid? LinkedProcessId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("justiceSecret")]
        public bool? JusticeSecret { get; set; }

        [JsonProperty("distributionDate")]
        public DateTime? DistributionDate { get; set; }

        [JsonProperty("clientPhysicalFolder")]
        public string ClientPhysicalFolder { get; set; }

        [JsonProperty("unifiedProcessNumber")]
        public string UnifiedProcessNumber { get; set; }

        [JsonProperty("situationId")]
        public Guid? SituationId { get; set; }

        [JsonProperty("responsibles")]
        public IList<Guid?> Responsibles { get; set; }

        [JsonProperty("updateUserName")]
        public string UpdateUserName { get; set; }
    }
}