using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Mappers
{
    public static class ProcessRequestTOMapper
    {
        public static ProcessEntity ToProcessEntity(this ProcessRequestTO processRequestTO)
        {
            if (processRequestTO == null)
            {
                return null;
            }

            return new ProcessEntity()
            {
                Description = processRequestTO.Description,
                SituationId = processRequestTO.SituationId,
                JusticeSecret = processRequestTO.JusticeSecret,
                UpdateUserName = processRequestTO.UpdateUserName,
                LinkedProcessId = processRequestTO.LinkedProcessId,
                DistributionDate = processRequestTO.DistributionDate,
                ClientPhysicalFolder = processRequestTO.ClientPhysicalFolder,
                UnifiedProcessNumber = processRequestTO.UnifiedProcessNumber,
                ProcessResponsible = ToProcessResponsibleEntity(processRequestTO.Responsibles)
            };
        }

        public static IList<ProcessResponsibleEntity> ToProcessResponsibleEntity(IList<Guid?> listResponsableId)
        {
            if (listResponsableId == null)
            {
                return null;
            }

            var processResponsible = new List<ProcessResponsibleEntity>();

            foreach (var responsibleId in listResponsableId)
            {
                if (responsibleId != null)
                {
                    processResponsible.Add(new ProcessResponsibleEntity
                    {
                        ResponsibleId = responsibleId
                    });
                }
            }

            return processResponsible.Any() ? processResponsible : null;
        }
    }
}