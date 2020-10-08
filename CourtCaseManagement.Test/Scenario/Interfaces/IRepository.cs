using CourtCaseManagement.Infrastructure.Data;

namespace CourtCaseManagement.Test.Scenario.Interfaces
{
    internal interface IRepository
    {
        CatalogContext CatalogContext { get; set; }
    }
}