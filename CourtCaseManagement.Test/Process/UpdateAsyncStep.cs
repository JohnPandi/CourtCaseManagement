using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using CourtCaseManagement.Test.Scenario;
using CourtCaseManagement.Test.Scenario.Helpers;
using CourtCaseManagement.Test.Scenario.WebApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Sdk;

namespace CourtCaseManagement.Test.Process
{
    [Binding]
    internal class ProcessUpdateAsync : BaseTest, IClassFixture<TestServerFixture>
    {
        public ProcessUpdateAsync(TestServerFixture factory) : base(factory)
        {

        }

        [BeforeScenario("@ProcessUpdateAsync")]
        public void BeforeScenario() => BaseBeforeScenario();

        [Given(@"o processo previamente cadastrado")]
        public async Task DadoOProcessoPreviamenteCadastrado(Table table)
        {
            foreach(var row in table.Rows)
            {
                Variable.UnifiedProcessNumber = row["unifiedProcessNumber"].Replace("\"", string.Empty);
                Variable.DistributionDate = row["distributionDate"].Replace("\"", string.Empty);
                Variable.JusticeSecret = row["justiceSecret"].Replace("\"", string.Empty);
                Variable.ClientPhysicalFolder = row["clientPhysicalFolder"].Replace("\"", string.Empty);
                Variable.Description = row["description"].Replace("\"", string.Empty);
                Variable.SituationId = row["situationId"].Replace("\"", string.Empty);
                Variable.ResponsiblesCpf = row["responsiblesCpf"].Replace("\"", string.Empty);
                Variable.ResponsiblesName = row["responsiblesName"].Replace("\"", string.Empty);
                Variable.ResponsiblesEMail = row["responsiblesEMail"].Replace("\"", string.Empty);
                Variable.Image = row["image"].Replace("\"", string.Empty);

                var processId = await CreateProcess();
                Variable.Clean();
                Variable.ProcessId = processId;
            }
        }
        
        [Given(@"atualizando a data de distribuição para ""(.*)""")]
        public void DadoAtualizandoADataDeDistribuicaoPara(string distributionDate)
        {
            Variable.DistributionDate = distributionDate.Replace("\"", string.Empty);
        }
        
        [Given(@"atualizando o segredo de justiça para ""(.*)""")]
        public void DadoAtualizandoOSegredoDeJusticaPara(string justiceSecret)
        {
            Variable.JusticeSecret = justiceSecret.Replace("\"", string.Empty);
        }
        
        [Given(@"atualizando a pasta física cliente para ""(.*)""")]
        public void DadoAtualizandoAPastaFisicaClientePara(string clientPhysicalFolder)
        {
            Variable.ClientPhysicalFolder = clientPhysicalFolder.Replace("\"", string.Empty);
        }
        
        [Given(@"atualizando a descrição para ""(.*)""")]
        public void DadoAtualizandoADescricaoPara(string description)
        {
            Variable.Description = description.Replace("\"", string.Empty);
        }
        
        [Given(@"atualizando a situação para ""(.*)""")]
        public void DadoAtualizandoASituacaoPara(string situationId)
        {
            Variable.SituationId = situationId.Replace("\"", string.Empty);
        }
        
        [When(@"solicitar o atualizar um processo")]
        public async Task QuandoSolicitarOAtualizarUmProcesso()
        {
            Variable.ProcessResponseTO = await HttpClientHelper.PutAsync<ProcessResponseTO>($"/courtCaseManagement/v1/process/" + Variable.ProcessId.Value.ToString(), await CreateProcessRequestTO());
        }

        [Given(@"atualizando o número do processo unificado ""(.*)""")]
        public void DadoAtualizandoONumeroDoProcessoUnificado(string unifiedProcessNumber)
        {
            Variable.UnifiedProcessNumber = unifiedProcessNumber.Replace("\"", string.Empty);
        }

        [Given(@"atualizando o responsavel ""(.*)"" ""(.*)"" ""(.*)"" ""(.*)""")]
        public void DadoAtualizandoOResponsavel(string cpf, string name, string mail, string image)
        {
            Variable.ResponsiblesCpf = cpf.Replace("\"", string.Empty);
            Variable.ResponsiblesName = name.Replace("\"", string.Empty);
            Variable.ResponsiblesEMail = mail.Replace("\"", string.Empty);
            Variable.Image = image.Replace("\"", string.Empty);
        }

        private async Task<ProcessRequestTO> CreateProcessRequestTO()
        {
            Guid? situationId = null;
            if (!string.IsNullOrEmpty(Variable.SituationId))
            {
                var situationRepository = Repository.CatalogContext.Set<SituationEntity>();
                situationId = ((await situationRepository.ToListAsync()).First(situation => situation.Name.Trim().ToUpper() == Variable.SituationId.Trim().ToUpper())).Id;
            }

            var responsibleRepository = Repository.CatalogContext.Set<ResponsibleEntity>();
            ResponsibleEntity responsibleEntity = null;
            if (!string.IsNullOrEmpty(Variable.ResponsiblesCpf) && !string.IsNullOrEmpty(Variable.ResponsiblesName) && !string.IsNullOrEmpty(Variable.ResponsiblesEMail) && !string.IsNullOrEmpty(Variable.Image))
            {
                responsibleEntity = (await responsibleRepository.AddAsync(new ResponsibleEntity
                {
                    Cpf = Convert.ToInt64(Variable.ResponsiblesCpf.Replace(".", "").Replace("-", "")),
                    Name = Variable.ResponsiblesName,
                    Mail = Variable.ResponsiblesEMail,
                    Photograph = Variable.Image,
                })).Entity;
            }

            await Repository.CatalogContext.SaveChangesAsync();

            return new ProcessRequestTO
            {
                Description = Variable.Description,
                UpdateUserName = "teste",
                DistributionDate = !string.IsNullOrEmpty(Variable.DistributionDate) ? (DateTime?)DateTime.ParseExact(Variable.DistributionDate, "yyyy-MM-dd", CultureInfo.CreateSpecificCulture("pt-BR")) : null,
                JusticeSecret = !string.IsNullOrEmpty(Variable.JusticeSecret) ? (bool?)(Variable.JusticeSecret == "S" ? true : false) : null,
                SituationId = situationId,
                UnifiedProcessNumber = Variable.UnifiedProcessNumber,
                ClientPhysicalFolder = Variable.ClientPhysicalFolder,
                Responsibles = responsibleEntity != null ? (new List<Guid?>
                {
                    responsibleEntity.Id
                }) : null
            };
        }

        private async Task<Guid?> CreateProcess()
        {
            Guid? situationId = null;
            if (!string.IsNullOrEmpty(Variable.SituationId))
            {
                var situationRepository = Repository.CatalogContext.Set<SituationEntity>();
                situationId = ((await situationRepository.ToListAsync()).First(situation => situation.Name.Trim().ToUpper() == Variable.SituationId.Trim().ToUpper())).Id;
            }

            var responsibleRepository = Repository.CatalogContext.Set<ResponsibleEntity>();
            ResponsibleEntity responsibleEntity = null;
            if (!string.IsNullOrEmpty(Variable.ResponsiblesCpf) && !string.IsNullOrEmpty(Variable.ResponsiblesName) && !string.IsNullOrEmpty(Variable.ResponsiblesEMail) && !string.IsNullOrEmpty(Variable.Image))
            {
                responsibleEntity = (await responsibleRepository.AddAsync(new ResponsibleEntity
                {
                    Cpf = Convert.ToInt64(Variable.ResponsiblesCpf.Replace(".", "").Replace("-", "")),
                    Name = Variable.ResponsiblesName,
                    Mail = Variable.ResponsiblesEMail,
                    Photograph = Variable.Image,
                })).Entity;
            }

            var processRepository = Repository.CatalogContext.Set<ProcessEntity>();
            var processEntity = (await processRepository.AddAsync(new ProcessEntity
            {
                JusticeSecret = (bool?)(Variable.JusticeSecret == "S" ? true : false),
                Description = Variable.Description,
                SituationId = situationId,
                UnifiedProcessNumber = Variable.UnifiedProcessNumber,
                ClientPhysicalFolder = Variable.ClientPhysicalFolder,
                UpdateDate = DateTime.Now.Date,
                UpdateUserName = "teste",
                DistributionDate = (DateTime?)DateTime.ParseExact(Variable.DistributionDate, "yyyy-MM-dd", CultureInfo.CreateSpecificCulture("pt-BR")),
                Version = 1,
                ProcessResponsible = new List<ProcessResponsibleEntity>() 
                { 
                    new ProcessResponsibleEntity
                    {
                        ResponsibleId = responsibleEntity.Id
                    }
                }
            })).Entity;

            await Repository.CatalogContext.SaveChangesAsync();

            return processEntity.Id;
        }
    }
}
