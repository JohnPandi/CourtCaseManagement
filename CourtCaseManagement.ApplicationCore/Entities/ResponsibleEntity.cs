using CourtCaseManagement.ApplicationCore.Interfaces;
using System;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class ResponsibleEntity : BaseEntity, IAggregateRoot
    {
        public virtual long? Cpf { get; set; }
        public virtual string Mail { get; set; }
        public virtual string Name { get; set; }
        public virtual string Photograph { get; set; }
        public virtual Guid? ProcessId { get; set; }
        public virtual ProcessEntity Process { get; set; }
    }
}