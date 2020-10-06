using Newtonsoft.Json;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ResponsibleRequestTO
    {
        [JsonProperty("cpf")]
        public long? Cpf { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photograph")]
        public string Photograph { get; set; }
    }
}