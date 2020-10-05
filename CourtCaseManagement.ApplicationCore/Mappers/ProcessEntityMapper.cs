using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;

namespace CourtCaseManagement.ApplicationCore.Mappers
{
    public static class ProcessEntityMapper
    {
        public static ProcessResponseTO ToProcessEntity(this ProcessEntity processEntity)
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
                Situation = processEntity.Situation.ToSituationEntity(),
                Responsibles = processEntity.Responsibles.ToListResponsibleEntity()
            };
        } 
    }
}