using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Facades
{
    public class ProcessFacade : IProcessFacade
    {
        private readonly IProcessService _processService;

        public ProcessFacade(IProcessService processService)
        {
            _processService = processService;
        }

        public async Task<ProcessResponseTO> AddAsync(ProcessRequestTO processRequestTO)
        {
            return await _processService.AddAsync(processRequestTO);
        }

        public async Task UpdateAsync(Guid? processId, ProcessRequestTO processRequestTO)
        {
            await _processService.UpdateAsync(processId, processRequestTO);
        }

        public async Task DeleteAsync(Guid? processId)
        {
            await _processService.DeleteAsync(processId);
        }

        public async Task<IList<ProcessResponseTO>> ListAsync(ProcessFilterTO filterTO)
        {
            return await _processService.ListAsync(filterTO);
        }
    }
}