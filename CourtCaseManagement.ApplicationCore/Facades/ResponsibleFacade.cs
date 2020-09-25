using CourtCaseManagement.ApplicationCore.Interfaces;

namespace CourtCaseManagement.ApplicationCore.Facades
{
    public class ResponsibleFacade : IResponsibleFacade
    {
        private readonly IProcessService _processService;

        public ResponsibleFacade(IProcessService processService)
        {
            _processService = processService;
        }
    }
}