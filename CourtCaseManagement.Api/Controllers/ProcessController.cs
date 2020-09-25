using CourtCaseManagement.ApplicationCore.Interfaces;
using CourtCaseManagement.ApplicationCore.TOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;

namespace CourtCaseManagement.Api.Controllers
{
    [ApiController]
    [Route("courtCaseManagement/process")]
    [Produces("application/json")]
    public class ProcessController : ControllerBase
    {
        private readonly ILogger<ProcessController> _logger;
        private readonly IProcessFacade _processFacade;

        public ProcessController(ILogger<ProcessController> logger, IProcessFacade processFacade)
        {
            _logger = logger;
            _processFacade = processFacade;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProcessResponseTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> AddAsync([FromBody][Required] ProcessRequestTO friendTO)
        {
            ProcessResponseTO response = null;

            try
            {
                response = await _processFacade.AddAsync(friendTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync", new object[] { friendTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Created(nameof(AddAsync), response);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ProcessResponseTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> ListAsync([FromQuery] ProcessFilterTO filterTO)
        {
            IList<ProcessResponseTO> friends = null;

            try
            {
                friends = await _processFacade.ListAsync(filterTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync", new object[] { filterTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Ok(friends);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")][Required] Guid? id, [FromBody][Required] string name)
        {
            try
            {
                await _processFacade.UpdateAsync(id, name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync", new object[] { id, name });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")][Required] Guid? id)
        {
            try
            {
                await _processFacade.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync", new object[] { id });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Ok();
        }
    }
}