using CourtCaseManagement.ApplicationCore.Entities;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface ISituationRepository : IAsyncRepository<SituationEntity>
    {
    }
}