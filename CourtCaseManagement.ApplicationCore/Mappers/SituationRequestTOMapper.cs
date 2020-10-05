using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using System.Collections.Generic;
using System.Linq;

namespace CourtCaseManagement.ApplicationCore.Mappers
{
    public static class SituationRequestTOMapper
    {
        public static SituationEntity ToSituationEntity(this SituationRequestTO situationRequestTO)
        {
            if (situationRequestTO == null)
            {
                return null;
            }

            return new SituationEntity()
            {
                Name = situationRequestTO.Name,
                Finished = situationRequestTO.Finished
            };
        }

        public static IList<SituationEntity> ToListSituationEntity(this IList<SituationRequestTO> listRequest)
        {
            if (listRequest == null)
            {                 
                return null;
            }

            var list = new List<SituationEntity>();

            listRequest.ToList().ForEach(request =>
            {
                if (request != null)
                {
                    list.Add(request.ToSituationEntity());
                }
            });

            return list;
        }
    }
}