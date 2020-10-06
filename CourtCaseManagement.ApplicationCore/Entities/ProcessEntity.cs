using CourtCaseManagement.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class ProcessEntity : BaseEntity, IAggregateRoot
    {
        public virtual int? Version { get; set; }
        public virtual DateTime? UpdateDate { get; set; }
        public virtual string UpdateUserName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool? JusticeSecret { get; set; }
        public virtual DateTime? DistributionDate { get; set; }
        public virtual string ClientPhysicalFolder { get; set; }
        public virtual string UnifiedProcessNumber { get; set; }
        public virtual Guid? SituationId { get; set; }
        public virtual SituationEntity Situation { get; set; }
        public virtual IList<ResponsibleEntity> Responsibles { get; set; }
    }
}