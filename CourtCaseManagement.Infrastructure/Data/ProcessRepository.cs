using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtCaseManagement.Infrastructure.Data
{
    public class ProcessRepository : EfRepository<ProcessEntity>, IProcessRepository
    {
        public ProcessRepository(CatalogContext dbContext) : base(dbContext)
        {

        }

        public async Task<ProcessEntity> GetByIdWithIncludesAsync(Guid id)
        {
            return await _dbContext.Set<ProcessEntity>()
                .Where(process => process.Id == id)
                .Include(process => process.Situation)
                .Include(process => process.ProcessResponsible)
                .FirstOrDefaultAsync();
        }
    }
}