using CourtCaseManagement.Api;
using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.Infrastructure.Data;
using CourtCaseManagement.Test.Scenario.Data;
using CourtCaseManagement.Test.Scenario.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CourtCaseManagement.Test.Scenario.WebApi
{
    internal class StartupCustom : Api.StartupCustom, IStartupCustom
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

                CadastrarTabelasDominio(scopedServices);
            }

            services.AddScoped<IRepository, Repository>();
        }

        private void CadastrarTabelasDominio(IServiceProvider serviceProvider)
        {
            CadastrarSituacoes(serviceProvider);
        }

        private void CadastrarSituacoes(IServiceProvider serviceProvider)
        {
            var situationRepository = serviceProvider.GetService<ISituationRepository>();
            situationRepository.AddAsync(new SituationEntity { Name = "Em andamento", Finished = false });
            situationRepository.AddAsync(new SituationEntity { Name = "Desmembrado", Finished = false });
            situationRepository.AddAsync(new SituationEntity { Name = "Em recurso", Finished = false });
            situationRepository.AddAsync(new SituationEntity { Name = "Finalizado", Finished = true });
            situationRepository.AddAsync(new SituationEntity { Name = "Arquivado", Finished = true });
        }
    }
}