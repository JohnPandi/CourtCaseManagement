using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using CourtCaseManagement.Test.Scenario;
using CourtCaseManagement.Test.Scenario.Helpers;
using CourtCaseManagement.Test.Scenario.WebApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace CourtCaseManagement.Test.Process
{
    [Binding]
    internal class ProcessListAsync : BaseTest, IClassFixture<TestServerFixture>
    {
        public ProcessListAsync(TestServerFixture factory) : base(factory)
        {

        }

        [BeforeScenario("@ProcessListAsync")]
        public void BeforeScenario() => BaseBeforeScenario();

        [Given(@"ao filtrar pelo número do processo unificado ""(.*)""")]
        public void DadoAoFiltrarPeloNumeroDoProcessoUnificado(string unifiedProcessNumber)
        {
            Variable.UnifiedProcessNumber = unifiedProcessNumber.Replace("\"", string.Empty);
        }
        
        [Given(@"ao filtrar pelo periodo de de distribuição ""(.*)"" ""(.*)""")]
        public void DadoAoFiltrarPeloPeriodoDeDeDistribuicao(string distributionDateStart, string distributionDateEnd)
        {
            Variable.DistributionDateStart = distributionDateStart.Replace("\"", string.Empty);
            Variable.DistributionDateEnd = distributionDateEnd.Replace("\"", string.Empty);
        }
        
        [Given(@"ao filtrar pelo segredo de justiça ""(.*)""")]
        public void DadoAoFiltrarPeloSegredoDeJustica(string justiceSecret)
        {
            Variable.JusticeSecret = justiceSecret.Replace("\"", string.Empty);
        }
        
        [Given(@"ao filtrar pela pasta física do cliente ""(.*)""")]
        public void DadoAoFiltrarPelaPastaFisicaDoCliente(string clientPhysicalFolder)
        {
            Variable.ClientPhysicalFolder = clientPhysicalFolder.Replace("\"", string.Empty);
        }
        
        [Given(@"ao filtrar pela situação para ""(.*)""")]
        public void DadoAoFiltrarPelaSituacaoPara(string situationId)
        {
            Variable.SituationId = situationId.Replace("\"", string.Empty);
        }
        
        [Given(@"ao filtrar pelo nome do responsável ""(.*)""")]
        public void DadoAoFiltrarPeloNomeDoResponsavel(string responsiblesName)
        {
            Variable.ResponsiblesName = responsiblesName.Replace("\"", string.Empty);
        }
        
        [When(@"solicitar a consulta dos processos")]
        public async Task QuandoSolicitarAConsultaDosProcessos()
        {
            Guid? situationId = null;
            if (!string.IsNullOrEmpty(Variable.SituationId))
            {
                var situationRepository = Repository.CatalogContext.Set<SituationEntity>();
                situationId = ((await situationRepository.ToListAsync()).First(situation => situation.Name.Trim().ToUpper() == Variable.SituationId.Trim().ToUpper())).Id;
            }

            var filtro = "?";
            filtro += !string.IsNullOrEmpty(Variable.ResponsiblesName) ? "&responsibleName=" + Variable.ResponsiblesName : string.Empty;
            filtro += situationId != null ? "&situationId=" + situationId : string.Empty;
            filtro += !string.IsNullOrEmpty(Variable.ClientPhysicalFolder) ? "&clientPhysicalFolder=" + Variable.ClientPhysicalFolder : string.Empty;
            filtro += !string.IsNullOrEmpty(Variable.JusticeSecret) ? "&justiceSecret=" + (Variable.JusticeSecret == "S" ? "true" : "false") : string.Empty;
            filtro += !string.IsNullOrEmpty(Variable.UnifiedProcessNumber) ? "&unifiedProcessNumber=" + Variable.UnifiedProcessNumber : string.Empty;
            filtro += !string.IsNullOrEmpty(Variable.DistributionDateStart) ? "&distributionDateStart=" + Variable.DistributionDateStart : string.Empty;
            filtro += !string.IsNullOrEmpty(Variable.DistributionDateEnd) ? "&distributionDateEnd=" + Variable.DistributionDateEnd : string.Empty;

            filtro = filtro.Replace("?&", "?");

            Variable.ListProcessResponseTO = await HttpClientHelper.GetAsync<IList<ProcessResponseTO>>($"/courtCaseManagement/v1/process" + filtro);
        }

        [Then(@"o sistema retornara (.*) de processos")]
        public void EntaoOSistemaRetornaraDeProcessos(int qtde)
        {
            Assert.Equal(qtde, Variable.ListProcessResponseTO.Count);
        }
    }
}