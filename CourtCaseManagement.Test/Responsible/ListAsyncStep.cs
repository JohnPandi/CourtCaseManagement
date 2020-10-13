using CourtCaseManagement.ApplicationCore.TOs;
using CourtCaseManagement.Test.Scenario;
using CourtCaseManagement.Test.Scenario.Helpers;
using CourtCaseManagement.Test.Scenario.WebApi;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace CourtCaseManagement.Test.Responsible
{
    [Binding]
    internal class ResponsibleListAsyncStep : BaseTest, IClassFixture<TestServerFixture>
    {
        public ResponsibleListAsyncStep(TestServerFixture factory) : base(factory)
        {

        }

        [BeforeScenario("@ResponsibleListAsync")]
        public void BeforeScenario() => BaseBeforeScenario();

        [Given(@"ao filtrar pelo cpf (.*)")]
        public void DadoAoFiltrarPeloCpf(string responsiblesCpf)
        {
            Variable.ResponsiblesCpf = responsiblesCpf.Replace("\"", string.Empty);
        }
        
        [Given(@"ao filtrar pela nome (.*)")]
        public void DadoAoFiltrarPelaNome(string responsiblesName)
        {
            Variable.ResponsiblesName = responsiblesName.Replace("\"", string.Empty);
        }
        
        [When(@"solicitar a consulta dos responsáveis")]
        public async Task QuandoSolicitarAConsultaDosResponsaveis()
        {
            var filtro = "?";
            filtro += !string.IsNullOrEmpty(Variable.UnifiedProcessNumber) ? "&unifiedProcessNumber=" + Variable.UnifiedProcessNumber : string.Empty;
            filtro += !string.IsNullOrEmpty(Variable.ResponsiblesCpf) ? "&cpf=" + Variable.ResponsiblesCpf : string.Empty;
            filtro += !string.IsNullOrEmpty(Variable.ResponsiblesName) ? "&name=" + Variable.ResponsiblesName : string.Empty;

            filtro = filtro.Replace("?&", "?");

            Variable.ListResponsibleResponseTO = await HttpClientHelper.GetAsync<IList<ResponsibleResponseTO>>($"/courtCaseManagement/v1/responsible" + filtro);
        }
        
        [Then(@"o sistema retornara (.*) de responsáveis")]
        public void EntaoOSistemaRetornaraDeResponsaveis(int qtde)
        {
            Assert.Equal(qtde, Variable.ListResponsibleResponseTO.Count);
        }
    }
}
