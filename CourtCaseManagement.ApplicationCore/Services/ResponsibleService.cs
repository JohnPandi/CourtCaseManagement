using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.ApplicationCore.Mappers;
using CourtCaseManagement.ApplicationCore.Specification;
using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourtCaseManagement.ApplicationCore.Services
{
    public class ResponsibleService : IResponsibleService
    {
        private readonly IResponsibleRepository _responsibleRepository;

        public ResponsibleService(IResponsibleRepository responsibleRepository)
        {
            _responsibleRepository = responsibleRepository;
        }

        public async Task<ResponsibleResponseTO> AddAsync(ResponsibleRequestTO responsibleRequestTO)
        {
            var responsibleEntity = await _responsibleRepository.AddAsync(responsibleRequestTO.ToResponsibleEntity());

            return responsibleEntity.ToResponsibleResponseTO();
        }

        public async Task UpdateAsync(Guid? responsibleId, ResponsibleRequestTO responsibleRequestTO)
        {
            var responsibleEntity = await _responsibleRepository.GetByIdAsync(responsibleId.Value);

            responsibleEntity.Cpf = responsibleRequestTO.Cpf;
            responsibleEntity.Name = responsibleRequestTO.Name;
            responsibleEntity.Photograph = responsibleRequestTO.Photograph;

            await _responsibleRepository.UpdateAsync(responsibleEntity);
        }

        public async Task DeleteAsync(Guid? responsibleId)
        {
            var responsibleEntity = await _responsibleRepository.GetByIdAsync(responsibleId.Value);
            await _responsibleRepository.DeleteAsync(responsibleEntity);
        }

        public async Task<IList<ResponsibleResponseTO>> ListAsync(ResponsibleFilterTO filterTO)
        {
            var responsibleSpecification = new BaseSpecification<ResponsibleEntity>();

            responsibleSpecification.AddCriteria(filterTO.UnifiedProcessNumber, responsible => responsible.ProcessResponsible.Any(processResponsible => processResponsible.Process != null && !string.IsNullOrEmpty(processResponsible.Process.UnifiedProcessNumber) && processResponsible.Process.UnifiedProcessNumber.Replace("-", string.Empty).Replace(".", string.Empty).Contains(filterTO.UnifiedProcessNumber.Replace("-", string.Empty).Replace(".", string.Empty))));
            responsibleSpecification.AddCriteria(filterTO.Cpf, responsible => responsible.Cpf == Convert.ToInt64(filterTO.Cpf.Replace(".", string.Empty).Replace("-", string.Empty)));
            responsibleSpecification.AddCriteria(filterTO.Name, responsible => responsible.Name.Trim().ToUpper().Contains(filterTO.Name.Trim().ToUpper()));

            responsibleSpecification.ApplyOrderByDescending(responsible => responsible.Name);
            responsibleSpecification.ApplyPaging(filterTO.Page.GetValueOrDefault(), filterTO.PerPage.GetValueOrDefault());

            var listResponsible = await _responsibleRepository.ListAsync(responsibleSpecification);

            return listResponsible.ToList().ToListResponsibleResponseTO();
        }
    }
}