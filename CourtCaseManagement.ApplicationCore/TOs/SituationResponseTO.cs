using System;

namespace CourtCaseManagement.ApplicationCore.TOs
{
    public class SituationResponseTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public bool? Finished { get; set; }
    }
}