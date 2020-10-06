using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.ApplicationCore.Mappers;
using CourtCaseManagement.ApplicationCore.TOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CourtCaseManagement.Api.Controllers
{
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("courtCaseManagement/v{version:apiVersion}/situation")]
    [Produces("application/json")]
    public class SituationController : ControllerBase
    {
        private readonly ILogger<SituationController> _logger;
        private readonly ISituationRepository _situationRepository;

        public SituationController(ILogger<SituationController> logger, ISituationRepository situationRepository)
        {
            _logger = logger;
            _situationRepository = situationRepository;
        }

        /// <summary>
        /// Consultar situações
        /// </summary>
        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SituationResponseTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> ListAsync()
        {
            IList<SituationResponseTO> listSituation = null;

            try
            {
                listSituation = (await _situationRepository.ListAllAsync()).ToList().ToListSituationResponseTO();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ListAsync", new object[0]);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Ok(listSituation);
        }
    }
}