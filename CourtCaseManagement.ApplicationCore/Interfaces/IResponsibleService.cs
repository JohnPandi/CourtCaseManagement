using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IResponsibleService
    {
        Task<ResponsibleResponseTO> AddAsync(ResponsibleRequestTO responsibleRequestTO);
        Task UpdateAsync(Guid? responsibleId, ResponsibleRequestTO responsibleRequestTO);
        Task DeleteAsync(Guid? responsibleId);
        Task<IList<ResponsibleResponseTO>> ListAsync(ResponsibleFilterTO filterTO);
    }
}