using Newtonsoft.Json;
using System;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ProcessFilterTO
    {
        [JsonProperty("unifiedProcessNumber")]
        public string UnifiedProcessNumber { get; set; }

        [JsonProperty("distributionDateStart")]
        public DateTime? DistributionDateStart { get; set; }

        [JsonProperty("distributionDateEnd")]
        public DateTime? DistributionDateEnd { get; set; }

        [JsonProperty("justiceSecret")]
        public bool? JusticeSecret { get; set; }

        [JsonProperty("clientPhysicalFolder")]
        public string ClientPhysicalFolder { get; set; }

        [JsonProperty("situationId")]
        public Guid? SituationId { get; set; }

        [JsonProperty("responsibleName")]
        public string ResponsibleName { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("perPage")]
        public int? PerPage { get; set; }
    }
}