using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Exceptions;
using CourtCaseManagement.ApplicationCore.Helpers;
using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.ApplicationCore.Mappers;
using CourtCaseManagement.ApplicationCore.Messages;
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

        public void ValidateAsync(ResponsibleRequestTO responsibleRequestTO)
        {
            var errors = new List<ErrorsTO>();

            errors.AddRange(RequiredFields(responsibleRequestTO));

            if (errors.Count == 0)
                errors.AddRange(MaximumSizeFields(responsibleRequestTO));

            if (errors.Count == 0)
                errors.AddRange(ValidateCpfAsync(responsibleRequestTO));

            if (errors.Count > 0)
                throw new ErrorsException(errors);
        }

        private List<ErrorsTO> ValidateCpfAsync(ResponsibleRequestTO responsibleRequestTO)
        {
            var errors = new List<ErrorsTO>();

            if (!ValidateCPF.IsCpf(responsibleRequestTO.Cpf.ToString().PadLeft(11, '0')))
            {
                errors.Add(new ErrorsTO
                {
                    Field = "CPF",
                    Validation = Messaging.InvalidCPF
                });
            }

            return errors;
        }

        private List<ErrorsTO> RequiredFields(ResponsibleRequestTO responsibleRequestTO)
        {
            var errors = new List<ErrorsTO>();

            if (string.IsNullOrEmpty(responsibleRequestTO.Name))
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Name",
                    Validation = Messaging.RequiredName
                });
            }

            if (string.IsNullOrEmpty(responsibleRequestTO.Mail))
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Mail",
                    Validation = Messaging.RequiredMail
                });
            }

            if (responsibleRequestTO.Cpf == null)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Cpf",
                    Validation = Messaging.RequiredCpf
                });
            }

            if (string.IsNullOrEmpty(responsibleRequestTO.Photograph))
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Photograph",
                    Validation = Messaging.RequiredPhotograph
                });
            }

            return errors;
        }

        private List<ErrorsTO> MaximumSizeFields(ResponsibleRequestTO responsibleRequestTO)
        {
            var errors = new List<ErrorsTO>();

            if (!string.IsNullOrEmpty(responsibleRequestTO.Name) && responsibleRequestTO.Name.Trim().Length > 150)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Name",
                    Validation = Messaging.MaximumSizeName
                });
            }

            if (!string.IsNullOrEmpty(responsibleRequestTO.Mail) && responsibleRequestTO.Mail.Trim().Length > 400)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Mail",
                    Validation = Messaging.MaximumSizeMail
                });
            }

            return errors;
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
            if (responsibleId == null)
            {
                return;
            }

            var responsibleEntity = await _responsibleRepository.GetByIdAsync(responsibleId.Value);

            if (responsibleEntity == null)
            {
                throw new NotFoundException("ResponsibleId", Messaging.NotFoundResponsible);
            }

            await _responsibleRepository.DeleteAsync(responsibleEntity);
        }

        public void ValidateListAsync(ResponsibleFilterTO filterTO)
        {
            var errors = new List<ErrorsTO>();

            if (filterTO.PerPage > 50)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "PerPage",
                    Validation = Messaging.ExceededMaximumValue
                });
            }

            if (errors.Count > 0)
                throw new ErrorsException(errors);
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