using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.Exceptions;
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
    public class ProcessService : IProcessService
    {
        private readonly IResponsibleRepository _responsibleRepository;
        private readonly ISituationRepository _situationRepository;
        private readonly IProcessRepository _processRepository;

        public ProcessService(IProcessRepository processRepository, ISituationRepository situationRepository, IResponsibleRepository responsibleRepository)
        {
            _processRepository = processRepository;
            _situationRepository = situationRepository;
            _responsibleRepository = responsibleRepository;
        }

        public async Task ValidateAddAsync(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            errors.AddRange(RequiredFields(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(FixedSizeFields(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(MaximumSizeFields(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(await ValidateDoubleUnifiedProcessNumber(null, processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(ValidateDistributionDate(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(await ValidateResponsibles(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(await ExistsSituationId(processRequestTO));

            if (errors.Count > 0)
                throw new ErrorsException(errors);
        }

        public async Task ValidateUpdateAsync(Guid? processId, ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            errors.AddRange(await RequiredProcessId(processId));

            if (errors.Count == 0)
                errors.AddRange(RequiredFields(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(FixedSizeFields(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(MaximumSizeFields(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(await ValidateDoubleUnifiedProcessNumber(processId, processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(ValidateDistributionDate(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(await ValidateResponsibles(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(await ValidateSituationId(processRequestTO));

            if (errors.Count == 0)
                errors.AddRange(await ValidateLinkedProcessId(processRequestTO));

            if (errors.Count > 0)
                throw new ErrorsException(errors);
        }

        private async Task<List<ErrorsTO>> RequiredProcessId(Guid? processId)
        {
            var errors = new List<ErrorsTO>();

            if (processId == null)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "ProcessId",
                    Validation = Messaging.RequiredProcessId
                });
            }
            else if((await _processRepository.GetByIdAsync(processId.Value)) == null)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "ProcessId",
                    Validation = Messaging.InvalidProcessId
                });
            }
            

            return errors;
        }

        private async Task<List<ErrorsTO>> ExistsSituationId(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            var situationEntity = await _situationRepository.GetByIdAsync(processRequestTO.SituationId.Value);

            if (situationEntity == null)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "SituationId",
                    Validation = Messaging.InvalidSituationId
                });
            }

            return errors;
        }

        private async Task<List<ErrorsTO>> ValidateSituationId(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            var situationEntity = await _situationRepository.GetByIdAsync(processRequestTO.SituationId.Value);

            if(situationEntity == null)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "SituationId",
                    Validation = Messaging.InvalidSituationId
                });
            }
            else if (situationEntity.Finished != null && situationEntity.Finished.Value)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "SituationId",
                    Validation = Messaging.RegistrationCannotBeEdited
                });
            }

            return errors;
        }

        private async Task<List<ErrorsTO>> ValidateResponsibles(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            foreach (var responsibleId in processRequestTO.Responsibles) 
            {
                if ((await _responsibleRepository.GetByIdAsync(responsibleId.Value)) == null)
                {
                    errors.Add(new ErrorsTO
                    {
                        Field = "Responsibles",
                        Validation = Messaging.InvalidResponsible
                    });
                }
            }

            if (processRequestTO.Responsibles.Count > 3)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Responsibles",
                    Validation = Messaging.ExceededLimitOfResponsible
                });
            }

            if (processRequestTO.Responsibles.GroupBy(responsibleId => responsibleId).ToList().Count != processRequestTO.Responsibles.Count)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Responsibles",
                    Validation = Messaging.DuplicateResponsible
                });
            }

            return errors;
        }

        private async Task<List<ErrorsTO>> ValidateLinkedProcessId(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            var processSpecification = new BaseSpecification<ProcessEntity>();
            processSpecification.AddCriteria(process => process.Id == processRequestTO.LinkedProcessId);
            var processFather = (await _processRepository.ListAsync(processSpecification)).FirstOrDefault();

            var cont = 0;
            while (processFather != null && processFather.LinkedProcessId != null)
            {
                cont++;
                processSpecification = new BaseSpecification<ProcessEntity>();
                processSpecification.AddCriteria(process => process.Id == processRequestTO.LinkedProcessId);
                processFather = (await _processRepository.ListAsync(processSpecification)).FirstOrDefault();
            }

            if (cont > 4)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "LinkedProcessId",
                    Validation = Messaging.ExceededLimitOfHierarchyLinkedProcessId
                });
            }

            return errors;
        }

        private List<ErrorsTO> ValidateDistributionDate(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            if(processRequestTO.DistributionDate != null && processRequestTO.DistributionDate.Value.Date > DateTime.Now.Date)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "DistributionDate",
                    Validation = Messaging.DistributionDateGreaterThanTheCurrentDate
                });
            }

            return errors;
        }

        private async Task<List<ErrorsTO>> ValidateDoubleUnifiedProcessNumber(Guid? processId, ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            var processSpecification = new BaseSpecification<ProcessEntity>();
            processSpecification.AddCriteria(processId, process => process.Id != processId);
            processSpecification.AddCriteria(process => process.UnifiedProcessNumber.Replace("-", string.Empty).Replace(".", string.Empty) == processRequestTO.UnifiedProcessNumber.Replace("-", string.Empty).Replace(".", string.Empty));
            var listProcess = await _processRepository.ListAsync(processSpecification);

            if (listProcess.Any())
            {
                errors.Add(new ErrorsTO
                {
                    Field = "UnifiedProcessNumber",
                    Validation = Messaging.DoubleUnifiedProcessNumber
                });
            }

            return errors;
        }

        private List<ErrorsTO> FixedSizeFields(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            if (processRequestTO.UnifiedProcessNumber.Replace("-", string.Empty).Replace(".", string.Empty).Trim().Length != 20)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "UnifiedProcessNumber",
                    Validation = Messaging.FixedSizeUnifiedProcessNumber
                });
            }

            return errors;
        }

        private List<ErrorsTO> MaximumSizeFields(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            if (!string.IsNullOrEmpty(processRequestTO.ClientPhysicalFolder) && processRequestTO.ClientPhysicalFolder.Trim().Length > 50)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "ClientPhysicalFolder",
                    Validation = Messaging.MaximumSizeClientPhysicalFolder
                });
            }

            if (!string.IsNullOrEmpty(processRequestTO.Description) && processRequestTO.Description.Trim().Length > 1000)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Description",
                    Validation = Messaging.MaximumSizeDescription
                });
            }

            return errors;
        }

        private List<ErrorsTO> RequiredFields(ProcessRequestTO processRequestTO)
        {
            var errors = new List<ErrorsTO>();

            if (string.IsNullOrEmpty(processRequestTO.UnifiedProcessNumber))
            {
                errors.Add(new ErrorsTO
                {
                    Field = "UnifiedProcessNumber",
                    Validation = Messaging.RequiredUnifiedProcessNumber
                });
            }

            if (processRequestTO.JusticeSecret == null)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "JusticeSecret",
                    Validation = Messaging.RequiredJusticeSecret
                });
            }

            if (processRequestTO.SituationId == null)
            {
                errors.Add(new ErrorsTO
                {
                    Field = "SituationId",
                    Validation = Messaging.RequiredSituationId
                });
            }

            if (processRequestTO.Responsibles == null || !processRequestTO.Responsibles.Any())
            {
                errors.Add(new ErrorsTO
                {
                    Field = "Responsibles",
                    Validation = Messaging.RequiredResponsibles
                });
            }

            return errors;
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
            processEntity.LinkedProcessId = processRequestTO.LinkedProcessId;
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
            if(processId == null)
            {
                return;
            }

            var processEntity = await _processRepository.GetByIdAsync(processId.Value);

            if (processEntity == null)
            {
                throw new NotFoundException("ProcessId", Messaging.NotFoundProcess);
            }

            await _processRepository.DeleteAsync(processEntity);
        }

        public void ValidateListAsync(ProcessFilterTO filterTO)
        {
            var errors = new List<ErrorsTO>();

            if(filterTO.PerPage > 50)
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