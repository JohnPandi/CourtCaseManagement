using System;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class SituationEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool? Finished { get; set; }
        public Guid? ProcessId { get; set; }
        public ProcessEntity Process { get; set; }
    }
}