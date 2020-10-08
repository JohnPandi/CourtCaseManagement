using CourtCaseManagement.ApplicationCore.TOs;
using CourtCaseManagement.Test.Scenario.Interfaces;
using System.Net;
using System.Net.Http;

namespace CourtCaseManagement.Test.Scenario
{
    internal static class Variable
    {
        public static IRepository Repository { get; set; }
        public static ErrorsResponseTO ErrorsResponse { get; set; }
        public static ErrorMessageTO ErrorMessage { get; set; }
        public static HttpStatusCode? StatusCode { get; set; }
        public static ErrorTraceTO ErrorTrace { get; set; }
        public static HttpClient Client { get; set; }
        public static string UnifiedProcessNumber { get; set; }
        public static string DistributionDate { get; set; }
        public static string JusticeSecret { get; set; }
        public static string ClientPhysicalFolder { get; set; }
        public static string Description { get; set; }
        public static string SituationId { get; set; }
        public static string ResponsiblesCpf { get; set; }
        public static string ResponsiblesName { get; set; }
        public static string ResponsiblesEMail { get; set; }
        public static string Image { get; set; }

        public static void Clean()
        {
            ErrorsResponse = null;
            ErrorMessage = null;
            ErrorTrace = null;
            StatusCode = null;

            UnifiedProcessNumber = null;
            ClientPhysicalFolder = null;
            ResponsiblesEMail = null;
            DistributionDate = null;
            ResponsiblesName = null;
            ResponsiblesCpf = null;
            JusticeSecret = null;
            Description = null;
            SituationId = null;
            Image = null;
        }
    }
}