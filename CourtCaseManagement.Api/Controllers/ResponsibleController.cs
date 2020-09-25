using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CourtCaseManagement.Api.Controllers
{
    [ApiController]
    [Route("courtCaseManagement/responsible")]
    [Produces("application/json")]
    public class ResponsibleController : ControllerBase
    {
        private readonly ILogger<ResponsibleController> _logger;

        public ResponsibleController(ILogger<ResponsibleController> logger)
        {
            _logger = logger;
        }
    }
}