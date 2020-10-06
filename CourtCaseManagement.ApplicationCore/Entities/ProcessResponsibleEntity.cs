using CourtCaseManagement.ApplicationCore.Interfaces;
using System;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class ProcessResponsibleEntity : BaseEntity, IAggregateRoot
    {
        public virtual Guid? ProcessId { get; set; }
        public virtual ProcessEntity Process { get; set; }

        public virtual Guid? ResponsibleId { get; set; }
        public virtual ResponsibleEntity Responsible { get; set; }
    }
}