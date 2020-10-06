using CourtCaseManagement.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity, IAggregateRoot
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> CountAsync(ISpecification<T> spec);
        Task<T> FirstOrDefaultAsync(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}