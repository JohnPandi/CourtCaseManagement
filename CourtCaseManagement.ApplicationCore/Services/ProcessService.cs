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
    public class ProcessService : IProcessService
    {
        private readonly IProcessRepository _processRepository;

        public ProcessService(IProcessRepository processRepository)
        {
            _processRepository = processRepository;
        }

        public async Task<ProcessResponseTO> AddAsync(ProcessRequestTO processRequestTO)
        {
            var processEntity = processRequestTO.ToProcessEntity();
            processEntity.Version = 1;
            processEntity.UpdateDate = DateTime.Now;

            processEntity = await _processRepository.AddAsync(processEntity);

            return processEntity.ToProcessResponseTO();
        }

        public async Task UpdateAsync(Guid? processId, ProcessRequestTO processRequestTO)
        {
            var processEntity = await _processRepository.GetByIdWithIncludesAsync(processId.Value);

            processEntity.UnifiedProcessNumber = processRequestTO.UnifiedProcessNumber;
            processEntity.ClientPhysicalFolder = processRequestTO.ClientPhysicalFolder;
            processEntity.DistributionDate = processRequestTO.DistributionDate;
            processEntity.JusticeSecret = processRequestTO.JusticeSecret;
            processEntity.Description = processRequestTO.Description;
            
            processEntity.SituationId = processRequestTO.SituationId;

            processEntity.ProcessResponsible = new List<ProcessResponsibleEntity>();
            foreach (var responsibleId in processRequestTO.Responsibles)
            {
                processEntity.ProcessResponsible.Add(new ProcessResponsibleEntity
                {
                    ResponsibleId = responsibleId
                });
            }

            processEntity.UpdateUserName = processRequestTO.UpdateUserName;
            processEntity.Version = processEntity.Version + 1;
            processEntity.UpdateDate = DateTime.Now;

            await _processRepository.UpdateAsync(processEntity);
        }

        public async Task DeleteAsync(Guid? processId)
        {
            var processEntity = await _processRepository.GetByIdAsync(processId.Value);
            await _processRepository.DeleteAsync(processEntity);
        }

        public async Task<IList<ProcessResponseTO>> ListAsync(ProcessFilterTO filterTO)
        {
            var processSpecification = new BaseSpecification<ProcessEntity>();

            processSpecification.AddCriteria(filterTO.UnifiedProcessNumber, process => process.UnifiedProcessNumber.Replace("-", string.Empty).Replace(".", string.Empty) == filterTO.UnifiedProcessNumber.Replace("-", string.Empty).Replace(".", string.Empty));
            processSpecification.AddCriteria(filterTO.DistributionDateStart, process => process.DistributionDate >= filterTO.DistributionDateStart);
            processSpecification.AddCriteria(filterTO.DistributionDateEnd, process => process.DistributionDate <= filterTO.DistributionDateEnd);
            processSpecification.AddCriteria(filterTO.JusticeSecret, process => process.JusticeSecret == filterTO.JusticeSecret);
            processSpecification.AddCriteria(filterTO.ClientPhysicalFolder, process => process.ClientPhysicalFolder.Trim().ToUpper().Contains(filterTO.ClientPhysicalFolder.Trim().ToUpper()));
            processSpecification.AddCriteria(filterTO.SituationId, process => process.SituationId == filterTO.SituationId);
            processSpecification.AddCriteria(filterTO.ResponsibleName, process => process.ProcessResponsible.Any(processResponsible => processResponsible.Responsible != null && !string.IsNullOrEmpty(processResponsible.Responsible.Name) && processResponsible.Responsible.Name.Trim().ToUpper().Contains(filterTO.ResponsibleName.Trim().ToUpper())));

            processSpecification.ApplyOrderByDescending(responsible => responsible.UnifiedProcessNumber);
            processSpecification.ApplyPaging(filterTO.Page.GetValueOrDefault(), filterTO.PerPage.GetValueOrDefault());

            var listProcess = await _processRepository.ListAsync(processSpecification);

            return listProcess.ToList().ToListProcessResponseTO();
        }
    }
}