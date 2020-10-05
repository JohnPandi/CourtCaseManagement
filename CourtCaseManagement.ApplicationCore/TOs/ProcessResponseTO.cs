using System;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ProcessResponseTO
    {
        public Guid? Id { get; set; }
        public int? Version { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUserName { get; set; }
        public string Description { get; set; }
        public bool? JusticeSecret { get; set; }
        public DateTime? DistributionDate { get; set; }
        public string ClientPhysicalFolder { get; set; }
        public string UnifiedProcessNumber { get; set; }
        public SituationResponseTO Situation { get; set; }
        public IList<ResponsibleResponseTO> Responsibles { get; set; }
    }
}