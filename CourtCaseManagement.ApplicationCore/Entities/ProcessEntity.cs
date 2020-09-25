using CourtCaseManagement.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class ProcessEntity : BaseEntity, IAggregateRoot
    {
        public string Description { get; set; }
        public bool? JusticeSecret { get; set; }
        public DateTime? DistributionDate { get; set; }
        public string ClientPhysicalFolder { get; set; }
        public string UnifiedProcessNumber { get; set; }
        public IList<SituationEntity> Situations { get; set; }
        public IList<ResponsibleEntity> Responsibles { get; set; }
    }
}