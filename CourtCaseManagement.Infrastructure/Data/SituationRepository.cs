using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Interfaces;

namespace CourtCaseManagement.Infrastructure.Data
{
    public class SituationRepository : EfRepository<SituationEntity>, ISituationRepository
    {
        public SituationRepository(CatalogContext dbContext) : base(dbContext)
        {

        }
    }
}