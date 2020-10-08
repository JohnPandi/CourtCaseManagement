using CourtCaseManagement.Test.Scenario.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CourtCaseManagement.Test.Scenario.WebApi
{
    internal class BaseTest
    {
        protected IRepository Repository
        {
            get
            {
                return Factory.Services.GetService<IRepository>();
            }
        }

        protected TestServerFixture Factory { get; }

        public BaseTest(TestServerFixture factory)
        {
            factory.CreateHttpClient();
            Factory = factory;
        }

        public void BaseBeforeScenario()
        {
            Variable.Clean();
        }
    }
}