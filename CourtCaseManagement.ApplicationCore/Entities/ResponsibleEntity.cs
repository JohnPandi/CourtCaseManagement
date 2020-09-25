using System;

namespace CourtCaseManagement.ApplicationCore.Entities
{
    public class ResponsibleEntity : BaseEntity
    {
        public int? Cpf { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Photograph { get; set; }
        public Guid? ProcessId { get; set; }
        public ProcessEntity Process { get; set; }
    }
}