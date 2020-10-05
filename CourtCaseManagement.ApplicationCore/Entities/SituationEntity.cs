using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class SituationEntity : BaseEntity
    {
        public string Name { get; set; }
        public bool? Finished { get; set; }
        public IList<ProcessEntity> Processes { get; set; }
    }
}