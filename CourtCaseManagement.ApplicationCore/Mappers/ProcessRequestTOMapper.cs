using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;
using System.Linq;

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
                JusticeSecret = processRequestTO.JusticeSecret,
                DistributionDate = processRequestTO.DistributionDate,
                ClientPhysicalFolder = processRequestTO.ClientPhysicalFolder,
                UnifiedProcessNumber = processRequestTO.UnifiedProcessNumber,
                Situation = ToSituationEntity(processRequestTO.SituationId),
                Responsibles = ToListResponsibleEntity(processRequestTO.Responsibles)
            };
        }

        public static SituationEntity ToSituationEntity(Guid? situationId)
        {
            if(situationId == null)
            {
                return null;
            }

            return new SituationEntity
            {
                Id = situationId.Value
            };
        }

        public static IList<ResponsibleEntity> ToListResponsibleEntity(IList<Guid?> responsibles)
        {
            if (responsibles == null || !responsibles.Any())
            {
                return null;
            }

            var list = new List<ResponsibleEntity>();

            responsibles.ToList().ForEach(responsibleId => 
            {
                if (responsibleId != null)
                {
                    list.Add(new ResponsibleEntity { Id = responsibleId.Value });
                }
            });

            return list.Any() ? list : null;
        }
    }
}