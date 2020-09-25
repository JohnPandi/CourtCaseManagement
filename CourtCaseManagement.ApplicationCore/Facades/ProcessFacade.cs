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

        public Task<ProcessResponseTO> AddAsync(ProcessRequestTO gameTO)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid? gameId, string name)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid? gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ProcessResponseTO>> ListAsync(ProcessFilterTO filterTO)
        {
            throw new NotImplementedException();
        }
    }
}