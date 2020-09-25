using CourtCaseManagement.ApplicationCore.Entities;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IProcessRepository : IAsyncRepository<ProcessEntity>
    {
    }
}