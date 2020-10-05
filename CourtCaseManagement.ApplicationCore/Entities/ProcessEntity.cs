using CourtCaseManagement.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class ProcessEntity : BaseEntity, IAggregateRoot
    {
        public int? Version { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserName { get; set; }
        public string Description { get; set; }
        public bool? JusticeSecret { get; set; }
        public DateTime? DistributionDate { get; set; }
        public string ClientPhysicalFolder { get; set; }
        public string UnifiedProcessNumber { get; set; }
        public Guid? SituationId { get; set; }
        public SituationEntity? Situation { get; set; }
        public IList<ResponsibleEntity> Responsibles { get; set; }
    }
}