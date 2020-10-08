using CourtCaseManagement.Infrastructure.Data;
using CourtCaseManagement.Test.Scenario.Interfaces;

namespace CourtCaseManagement.Test.Scenario.Data
{
    internal class Repository : IRepository
    {
        public CatalogContext CatalogContext { get; set; }

        public Repository(CatalogContext dbContext)
        {
            CatalogContext = dbContext;
        }
    }
}