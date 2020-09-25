using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Interfaces;

namespace CourtCaseManagement.Infrastructure.Data
{
    public class ProcessRepository : EfRepository<ProcessEntity>, IProcessRepository
    {
        public ProcessRepository(CatalogContext dbContext) : base(dbContext)
        {

        }
    }
}