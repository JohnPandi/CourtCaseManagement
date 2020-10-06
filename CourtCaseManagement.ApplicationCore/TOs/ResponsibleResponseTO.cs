using System;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class ResponsibleResponseTO
    {
        public Guid? Id { get; set; }
        public long? Cpf { get; set; }
        public string Mail { get; set; }
        public string Name { get; set; }
        public string Photograph { get; set; }
    }
}