using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Facades
{
    public class ResponsibleFacade : IResponsibleFacade
    {
        private readonly IResponsibleService _responsibleService;

        public ResponsibleFacade(IResponsibleService responsibleService)
        {
            _responsibleService = responsibleService;
        }

        public async Task<ResponsibleResponseTO> AddAsync(ResponsibleRequestTO responsibleRequestTO)
        {
             _responsibleService.ValidateAsync(responsibleRequestTO);
            return await _responsibleService.AddAsync(responsibleRequestTO);
        }

        public async Task UpdateAsync(Guid? responsibleId, ResponsibleRequestTO responsibleRequestTO)
        {
            _responsibleService.ValidateAsync(responsibleRequestTO);
            await _responsibleService.UpdateAsync(responsibleId, responsibleRequestTO);
        }

        public async Task DeleteAsync(Guid? responsibleId)
        {
            await _responsibleService.DeleteAsync(responsibleId);
        }

        public async Task<IList<ResponsibleResponseTO>> ListAsync(ResponsibleFilterTO filterTO)
        {
            _responsibleService.ValidateListAsync(filterTO);
            return await _responsibleService.ListAsync(filterTO);
        }
    }
}