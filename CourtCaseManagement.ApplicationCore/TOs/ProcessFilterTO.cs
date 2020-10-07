using Newtonsoft.Json;
using System;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ProcessFilterTO
    {
        private const int DEFAULT_PAGE_VALUE = 1;
        private const int DEFAULT_PER_PAGE_VALUE = 50;
        private int? _page;
        private int? _perPage;

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
        public int? Page
        {
            get
            {
                return (int?)(_page == null ? DEFAULT_PAGE_VALUE : _page);
            }
            set
            {
                _page = value;
            }
        }

        [JsonProperty("perPage")]
        public int? PerPage
        {
            get
            {
                return (int?)(_perPage == null ? DEFAULT_PER_PAGE_VALUE : _perPage);
            }
            set
            {
                _perPage = value;
            }
        }
    }
}