using Newtonsoft.Json;
using System;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ResponsibleFilterTO
    {
        private const int DEFAULT_PAGE_VALUE = 1;
        private const int DEFAULT_PER_PAGE_VALUE = 50;
        private int? _page;
        private int? _perPage;

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unifiedProcessNumber")]
        public string UnifiedProcessNumber { get; set; }

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