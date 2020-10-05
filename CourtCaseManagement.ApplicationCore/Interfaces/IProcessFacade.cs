using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IProcessFacade
    {
        Task<ProcessResponseTO> AddAsync(ProcessRequestTO processRequestTO);
        Task<IList<ProcessResponseTO>> ListAsync(ProcessFilterTO filterTO);
        Task UpdateAsync(Guid? processId, ProcessRequestTO processRequestTO);
        Task DeleteAsync(Guid? processId);
    }
}