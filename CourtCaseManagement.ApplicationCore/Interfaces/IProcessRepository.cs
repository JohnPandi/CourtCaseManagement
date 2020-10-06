using CourtCaseManagement.ApplicationCore.Entities;
using System;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IProcessRepository : IAsyncRepository<ProcessEntity>
    {
        Task<ProcessEntity> GetByIdWithIncludesAsync(Guid id);
    }
}