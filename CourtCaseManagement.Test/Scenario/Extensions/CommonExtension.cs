using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace CourtCaseManagement.Test.Scenario.Extensions
{
    internal static class CommonExtension
    {
        public static void AddTransientMock<I, C>(this IServiceCollection services, C c) where I : class where C : I
        {
            var descriptor = services.FirstOrDefault(d => d.ServiceType == typeof(I));
            services.Remove(descriptor);

            services.AddTransient<I>(s => c);
        }

        public static StringContent ToStringContent(this object o)
        {
            return new StringContent(
                JsonConvert.SerializeObject(o),
                Encoding.UTF8,
                "application/json");
        }
    }
}