using CourtCaseManagement.Test.Scenario.Extensions;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;

namespace CourtCaseManagement.Test.Scenario.Helpers
{
    public static class HttpClientHelper
    {
        public static async Task<T> GetAsync<T>(string uri, string token) where T : class
        {
            Variable.Client.DefaultRequestHeaders.Clear();
            Variable.Client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await Variable.Client.GetAsync(uri);

            var stringResponse = await response.Content.ReadAsStringAsync();

            Variable.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<T>(stringResponse);
            }
            else
            {
                LoadError(stringResponse);

                return null;
            }
        }

        public static async Task<T> PostAsync<T>(string uri, string token, object o) where T : class
        {
            Variable.Client.DefaultRequestHeaders.Clear();
            Variable.Client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await Variable.Client.PostAsync(uri, o.ToStringContent());

            var stringResponse = await response.Content.ReadAsStringAsync();

            Variable.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<T>(stringResponse);
            }
            else
            {
                LoadError(stringResponse);

                return null;
            }
        }

        public static async Task<T> PutAsync<T>(string uri, object body, string token) where T : class
        {
            Variable.Client.DefaultRequestHeaders.Clear();
            Variable.Client.DefaultRequestHeaders.Add("Authorization", token);

            var response = await Variable.Client.PutAsync(uri, body.ToStringContent());

            var stringResponse = await response.Content.ReadAsStringAsync();

            Variable.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                return JsonConvert.DeserializeObject<T>(stringResponse);
            }
            else
            {
                LoadError(stringResponse);

                return null;
            }
        }

        private static void LoadError(string stringResponse)
        {
            if (Variable.StatusCode == HttpStatusCode.NotFound)
            {
                //Variable.ErrorMessage = JsonConvert.DeserializeObject<ErrorMessageTO>(stringResponse);
            }
            if (Variable.StatusCode == HttpStatusCode.BadRequest)
            {
                //Variable.ErrorsResponse = JsonConvert.DeserializeObject<ErrorsResponseTO>(stringResponse);
            }
            if (Variable.StatusCode == HttpStatusCode.InternalServerError)
            {
                //Variable.ErrorTrace = JsonConvert.DeserializeObject<ErrorTraceTO>(stringResponse);
            }
        }
    }
}
