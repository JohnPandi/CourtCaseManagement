using System.Net;
using System.Net.Http;

namespace CourtCaseManagement.Test.Scenario
{
    public static class Variable
    {
        public static HttpStatusCode? StatusCode { get; set; }
        public static HttpClient Client { get; set; }

        public static void Clean()
        {
            StatusCode = null;
            Client = null;
        }
    }
}