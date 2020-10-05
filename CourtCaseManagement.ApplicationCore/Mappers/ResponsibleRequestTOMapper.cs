using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using System.Collections.Generic;
using System.Linq;

namespace CourtCaseManagement.ApplicationCore.Mappers
{
    public static class ResponsibleRequestTOMapper
    {
        public static ResponsibleEntity ToResponsibleEntity(this ResponsibleRequestTO responsibleRequestTO)
        {
            if (responsibleRequestTO == null)
            {
                return null;
            }

            return new ResponsibleEntity()
            {
                Cpf = responsibleRequestTO.Cpf,
                Mail = responsibleRequestTO.Mail,
                Name = responsibleRequestTO.Name,
                Photograph = responsibleRequestTO.Photograph
            };
        }

        public static IList<ResponsibleEntity> ToListResponsibleEntity(this IList<ResponsibleRequestTO> listRequest)
        {
            if (listRequest == null)
            {
                return null;
            }

            var list = new List<ResponsibleEntity>();

            listRequest.ToList().ForEach(request =>
            {
                if (request != null)
                {
                    list.Add(request.ToResponsibleEntity());
                }
            });

            return list;
        }
    }
}