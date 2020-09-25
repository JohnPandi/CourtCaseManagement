using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IProcessFacade
    {
        Task<ProcessResponseTO> AddAsync(ProcessRequestTO gameTO);
        Task<IList<ProcessResponseTO>> ListAsync(ProcessFilterTO filterTO);
        Task UpdateAsync(Guid? gameId, string name);
        Task DeleteAsync(Guid? gameId);
    }
}