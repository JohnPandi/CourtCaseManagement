using CourtCaseManagement.Api;
using CourtCaseManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace CourtCaseManagement.Test.Scenario.WebApi
{
    public class StartupCustom : Api.StartupCustom, IStartupCustom
    {
        public override void AddDatabaseConfigure(IServiceCollection services)
        {
            services.AddEntityFrameworkInMemoryDatabase();

            var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(provider);
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            var sp = services.BuildServiceProvider();

            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<CatalogContext>();

                db.Database.EnsureCreated();
            }
        }
    }
}