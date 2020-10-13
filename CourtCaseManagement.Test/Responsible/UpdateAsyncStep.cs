using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using CourtCaseManagement.Test.Scenario;
using CourtCaseManagement.Test.Scenario.Helpers;
using CourtCaseManagement.Test.Scenario.WebApi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;

namespace CourtCaseManagement.Test.Responsible
{
    [Binding]
    internal class ResponsibleUpdateAsync : BaseTest, IClassFixture<TestServerFixture>
    {
        public ResponsibleUpdateAsync(TestServerFixture factory) : base(factory)
        {

        }

        [BeforeScenario("@ResponsibleUpdateAsync")]
        public void BeforeScenario() => BaseBeforeScenario();

        [Given(@"um responsável previamente cadastrado")]
        public async Task DadoUmResponsavelPreviamenteCadastrado(Table table)
        {
            foreach (var row in table.Rows)
            {
                Variable.ResponsiblesCpf = row["responsiblesCpf"].Replace("\"", string.Empty);
                Variable.ResponsiblesName = row["responsiblesName"].Replace("\"", string.Empty);
                Variable.ResponsiblesEMail = row["responsiblesEMail"].Replace("\"", string.Empty);
                Variable.Image = row["image"].Replace("\"", string.Empty);
                
                var responsibleId = await CreateResponsible();
                Variable.Clean();
                Variable.ResponsibleId = responsibleId;
            }
        }

        [Given(@"atualizando o cpf para ""(.*)""")]
        public void DadoAtualizandoOCpfPara(string cpf)
        {
            Variable.ResponsiblesCpf = cpf.Replace("\"", string.Empty);
        }
        
        [Given(@"atualizando o nome para ""(.*)""")]
        public void DadoAtualizandoONomePara(string name)
        {
            Variable.ResponsiblesName = name.Replace("\"", string.Empty);
        }
        
        [Given(@"atualizando o e-mail para ""(.*)""")]
        public void DadoAtualizandoOE_MailPara(string mail)
        {
            Variable.ResponsiblesEMail = mail.Replace("\"", string.Empty);
        }
        
        [Given(@"atualizando a foto para ""(.*)""")]
        public void DadoAtualizandoAFotoPara(string image)
        {
            Variable.Image = image.Replace("\"", string.Empty);
        }
        
        [When(@"solicitar o atualizar um responsável")]
        public async Task QuandoSolicitarOAtualizarUmResponsavel()
        {
            Variable.ResponsibleResponseTO = await HttpClientHelper.PutAsync<ResponsibleResponseTO>($"/courtCaseManagement/v1/responsible/" + Variable.ResponsibleId, CreateResponsibleRequestTO());
        }

        private ResponsibleRequestTO CreateResponsibleRequestTO() =>
            new ResponsibleRequestTO
            {
                Cpf = !string.IsNullOrEmpty(Variable.ResponsiblesCpf) ? (long?)Convert.ToInt64(Variable.ResponsiblesCpf.Replace(".", "").Replace("-", "")) : null,
                Name = Variable.ResponsiblesName,
                Mail = Variable.ResponsiblesEMail,
                Photograph = Variable.Image,
            };

        private async Task<Guid?> CreateResponsible()
        {
            var responsibleRepository = Repository.CatalogContext.Set<ResponsibleEntity>();
            var responsibleEntity = (await responsibleRepository.AddAsync(new ResponsibleEntity
            {
                Cpf = Convert.ToInt64(Variable.ResponsiblesCpf.Replace(".", "").Replace("-", "")),
                Name = Variable.ResponsiblesName,
                Mail = Variable.ResponsiblesEMail,
                Photograph = Variable.Image,
            })).Entity;

            await Repository.CatalogContext.SaveChangesAsync();

            return responsibleEntity.Id;
        }
    }
}
