using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using System.Collections.Generic;
using System.Linq;

namespace CourtCaseManagement.ApplicationCore.Mappers
{
    public static class ProcessEntityMapper
    {
        public static ProcessResponseTO ToProcessResponseTO(this ProcessEntity processEntity)
        {
            if (processEntity == null)
            {
                return null;
            }

            return new ProcessResponseTO()
            {
                Id = processEntity.Id,
                
                Version = processEntity.Version,
                UpdateDate = processEntity.UpdateDate,
                UpdateUserName = processEntity.UpdateUserName,

                Description = processEntity.Description,
                JusticeSecret = processEntity.JusticeSecret,
                DistributionDate = processEntity.DistributionDate,
                ClientPhysicalFolder = processEntity.ClientPhysicalFolder,
                UnifiedProcessNumber = processEntity.UnifiedProcessNumber,
                Situation = processEntity.Situation.ToSituationResponseTO(),
                //Responsible = processEntity.Responsible.ToResponsibleResponseTO()
            };
        }

        public static IList<ProcessResponseTO> ToListProcessResponseTO(this IList<ProcessEntity> listEntity)
        {
            if (listEntity == null)
            {
                return null;
            }

            var list = new List<ProcessResponseTO>();

            listEntity.ToList().ForEach(entity =>
            {
                if (entity != null)
                {
                    list.Add(entity.ToProcessResponseTO());
                }
            });

            return list;
        }
    }
}