using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Facades
{
    public class ResponsibleFacade : IResponsibleFacade
    {
        private readonly IProcessService _processService;

        public ResponsibleFacade(IProcessService processService)
        {
            _processService = processService;
        }

        public Task<ResponsibleResponseTO> AddAsync(ResponsibleRequestTO responsibleRequestTO)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Guid? gameId, ResponsibleRequestTO responsibleRequestTO)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid? gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ResponsibleResponseTO>> ListAsync(ResponsibleFilterTO filterTO)
        {
            throw new NotImplementedException();
        }
    }
}