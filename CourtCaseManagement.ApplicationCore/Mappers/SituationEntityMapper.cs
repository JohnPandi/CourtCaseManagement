using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using System.Collections.Generic;
using System.Linq;

namespace CourtCaseManagement.ApplicationCore.Mappers
{
    public static class SituationEntityMapper
    {
        public static SituationResponseTO ToSituationResponseTO(this SituationEntity situationEntity)
        {
            if (situationEntity == null)
            {
                return null;
            }

            return new SituationResponseTO()
            {
                Id = situationEntity.Id,
                Name = situationEntity.Name,
                Finished = situationEntity.Finished
            };
        }

        public static IList<SituationResponseTO> ToListSituationResponseTO(this IList<SituationEntity> listEntity)
        {
            if (listEntity == null)
            {                 
                return null;
            }

            var list = new List<SituationResponseTO>();

            listEntity.ToList().ForEach(entity =>
            {
                if (entity != null)
                {
                    list.Add(entity.ToSituationResponseTO());
                }
            });

            return list;
        }
    }
}