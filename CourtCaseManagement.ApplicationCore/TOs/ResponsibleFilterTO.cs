using Newtonsoft.Json;
using System;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ResponsibleFilterTO
    {
        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("linkedProcessId")]
        public Guid? LinkedProcessId { get; set; }

        [JsonProperty("page")]
        public int? Page { get; set; }

        [JsonProperty("perPage")]
        public int? PerPage { get; set; }
    }
}