using CourtCaseManagement.ApplicationCore.TOs;
using CourtCaseManagement.Test.Scenario;
using CourtCaseManagement.Test.Scenario.Helpers;
using CourtCaseManagement.Test.Scenario.WebApi;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace CourtCaseManagement.Test.Responsible
{
    [Binding]
    internal class ResponsibleAddAsync : BaseTest, IClassFixture<TestServerFixture>
    {
        public ResponsibleAddAsync(TestServerFixture factory) : base(factory)
        {

        }

        [BeforeScenario("@ResponsibleAddAsync")]
        public void BeforeScenario() => BaseBeforeScenario();

        [Given(@"o cadastro de um novo responsável com o cpf ""(.*)""")]
        public void DadoOCadastroDeUmNovoResponsavelComOCpf(string cpf)
        {
            Variable.ResponsiblesCpf = cpf.Replace("\"", string.Empty);
        }
        
        [Given(@"com o nome ""(.*)""")]
        public void DadoComONome(string name)
        {
            Variable.ResponsiblesName = name.Replace("\"", string.Empty);
        }
        
        [Given(@"com o e-mail ""(.*)""")]
        public void DadoComOE_Mail(string mail)
        {
            Variable.ResponsiblesEMail = mail.Replace("\"", string.Empty);
        }
        
        [Given(@"com a foto ""(.*)""")]
        public void DadoComAFoto(string image)
        {
            Variable.Image = image.Replace("\"", string.Empty);
        }
        
        [When(@"solicitar o cadastro do responsável")]
        public async Task QuandoSolicitarOCadastroDoResponsavel()
        {
            Variable.ResponsibleResponseTO = await HttpClientHelper.PostAsync<ResponsibleResponseTO>($"/courtCaseManagement/v1/responsible", CreateResponsible());
        }

        private ResponsibleRequestTO CreateResponsible() =>
            new ResponsibleRequestTO
            {
                Cpf = !string.IsNullOrEmpty(Variable.ResponsiblesCpf) ? (long?)Convert.ToInt64(Variable.ResponsiblesCpf.Replace(".", "").Replace("-", "")) : null,
                Name = Variable.ResponsiblesName,
                Mail = Variable.ResponsiblesEMail,
                Photograph = Variable.Image,
            };
    }
}