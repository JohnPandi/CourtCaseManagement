using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IProcessService
    {
        Task ValidateAddAsync(ProcessRequestTO processRequestTO);
        Task<ProcessResponseTO> AddAsync(ProcessRequestTO processRequestTO);
        Task UpdateAsync(Guid? processId, ProcessRequestTO processRequestTO);
        Task DeleteAsync(Guid? processId);
        Task<IList<ProcessResponseTO>> ListAsync(ProcessFilterTO filterTO);
        Task ValidateUpdateAsync(Guid? processId, ProcessRequestTO processRequestTO);
        void ValidateListAsync(ProcessFilterTO filterTO);
    }
}