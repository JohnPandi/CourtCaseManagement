using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CourtCaseManagement.Infrastructure.Data
{
    public class ResponsibleRepository : EfRepository<ResponsibleEntity>, IResponsibleRepository
    {
        public ResponsibleRepository(CatalogContext dbContext) : base(dbContext)
        {

        }

        public async Task<ResponsibleEntity> GetByIdWithIncludesAsync(Guid id)
        {
            return await _dbContext.Set<ResponsibleEntity>()
                .Where(responsible => responsible.Id == id)
                .Include(responsible => responsible.Process)
                .FirstOrDefaultAsync();
        }
    }
}