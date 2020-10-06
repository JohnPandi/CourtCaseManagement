using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IProcessService
    {
        Task<ProcessResponseTO> AddAsync(ProcessRequestTO processRequestTO);
        Task UpdateAsync(Guid? processId, ProcessRequestTO processRequestTO);
        Task DeleteAsync(Guid? processId);
        Task<IList<ProcessResponseTO>> ListAsync(ProcessFilterTO filterTO);
    }
}