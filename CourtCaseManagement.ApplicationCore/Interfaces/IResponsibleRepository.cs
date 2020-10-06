using CourtCaseManagement.ApplicationCore.Entities;
using System;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IResponsibleRepository : IAsyncRepository<ResponsibleEntity>
    {
        Task<ResponsibleEntity> GetByIdWithIncludesAsync(Guid id);
    }
}