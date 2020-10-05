using CourtCaseManagement.ApplicationCore.Entities;
using CourtCaseManagement.ApplicationCore.TOs;
using System.Collections.Generic;
using System.Linq;

namespace CourtCaseManagement.ApplicationCore.Mappers
{
    public static class ResponsibleEntityMapper
    {
        public static ResponsibleResponseTO ToResponsibleEntity(this ResponsibleEntity responsibleEntity)
        {
            if (responsibleEntity == null)
            {
                return null;
            }

            return new ResponsibleResponseTO()
            {
                Id = responsibleEntity.Id,
                Cpf = responsibleEntity.Cpf,
                Name = responsibleEntity.Name,
                Mail = responsibleEntity.Mail,
                Photograph = responsibleEntity.Photograph,
            };
        }

        public static IList<ResponsibleResponseTO> ToListResponsibleEntity(this IList<ResponsibleEntity> listEntity)
        {
            if (listEntity == null)
            {
                return null;
            }

            var list = new List<ResponsibleResponseTO>();

            listEntity.ToList().ForEach(entity =>
            {
                if (entity != null)
                {
                    list.Add(entity.ToResponsibleEntity());
                }
            });

            return list;
        }
    }
}