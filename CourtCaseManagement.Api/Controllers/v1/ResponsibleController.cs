using CourtCaseManagement.ApplicationCore.Exceptions;
using CourtCaseManagement.ApplicationCore.Helpers;
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
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    [Route("courtCaseManagement/v{version:apiVersion}/responsible")]
    [Produces("application/json")]
    public class ResponsibleController : ControllerBase
    {
        private readonly ILogger<ResponsibleController> _logger;
        private readonly IResponsibleFacade _responsibleFacade;

        public ResponsibleController(ILogger<ResponsibleController> logger, IResponsibleFacade responsibleFacade)
        {
            _logger = logger;
            _responsibleFacade = responsibleFacade;
        }

        /// <summary>
        /// Cadastrar responsável
        /// </summary>
        /// <remarks>
        ///**Modelo de Dados** *(corpo da requisição, body)*
        ///|Parâmetro|||Obrigatório|Tipo|Tamanho|Descrição|
        ///|---|---|---|:---:|:---:|---|---|
        ///|**cpf**|||✔️|long||<span>CPF.</span>|
        ///|**mail**|||✔️|string|400|<span>E-mail.</span>|
        ///|**name**|||✔️|string|150|<span>Nome.</span>|
        ///|**photograph**|||✔️|string||<span>Foto.</span>|
        /// </remarks>
        /// <returns>ResponsibleResponseTO</returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponsibleResponseTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessageTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsResponseTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorTraceTO))]
        public async Task<IActionResult> AddAsync([FromBody][Required] ResponsibleRequestTO responsibleRequestTO)
        {
            ResponsibleResponseTO responseTO = null;

            try
            {
                responseTO = await _responsibleFacade.AddAsync(responsibleRequestTO);
            }
            catch (ErrorsException ex)
            {
                return BadRequest(ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync", new object[] { responsibleRequestTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorHelper.GenericError(ex));
            }

            return Created(nameof(AddAsync), responseTO);
        }

        /// <summary>
        /// Consultar responsáveis
        /// </summary>
        /// <remarks>
        ///**Modelo de Dados** *(corpo da requisição, body)*
        ///|Parâmetro|||Obrigatório|Tipo|Tamanho|Descrição|
        ///|---|---|---|:---:|:---:|---|---|
        ///|**cpf**|||❌|string||<span>CPF.</span>|
        ///|**name**|||❌|string|150|<span>Nome.</span>|
        ///|**unifiedProcessNumber**|||❌|string||<span>Número do processo unificado.</span>|
        ///|**page**|||❌|int||<span>Número da Página.</span><br/><span>**Valor Padrão**: 1</span>|
        ///|**perPage**|||❌|int||<span>Quantidade de registros por página.</span><br/><span>**Valor Padrão**: 10</span><br/><span>**Valor Máximo**: 50</span>|
        /// </remarks>
        /// <returns>ResponsibleResponseTO</returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ResponsibleResponseTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessageTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsResponseTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorTraceTO))]
        public async Task<IActionResult> ListAsync([FromQuery] ResponsibleFilterTO filterTO)
        {
            IList<ResponsibleResponseTO> listResponseTO = null;

            try
            {
                listResponseTO = await _responsibleFacade.ListAsync(filterTO);
            }
            catch (ErrorsException ex)
            {
                return BadRequest(ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync", new object[] { filterTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorHelper.GenericError(ex));
            }

            return Ok(listResponseTO);
        }

        /// <summary>
        /// Editar responsável
        /// </summary>
        /// <remarks>
        ///**Modelo de Dados** *(corpo da requisição, body)*
        ///|Parâmetro|||Obrigatório|Tipo|Tamanho|Descrição|
        ///|---|---|---|:---:|:---:|---|---|
        ///|**cpf**|||❌|int||<span>CPF.</span>|
        ///|**mail**|||❌|string|400|<span>E-mail.</span>|
        ///|**name**|||❌|string|400|<span>Nome.</span>|
        ///|**photograph**|||❌|string||<span>Foto.</span>|
        /// </remarks>
        /// <param name="id">Identificador do responsável</param>
        /// <returns>ResponsibleResponseTO</returns>
        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessageTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsResponseTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorTraceTO))]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")][Required] Guid? id, [FromBody][Required] ResponsibleRequestTO responsibleRequestTO)
        {
            try
            {
                await _responsibleFacade.UpdateAsync(id, responsibleRequestTO);
            }
            catch (ErrorsException ex)
            {
                return BadRequest(ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync", new object[] { id, responsibleRequestTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorHelper.GenericError(ex));
            }

            return Ok();
        }

        /// <summary>
        /// Remover responsável
        /// </summary>
        /// <param name="id">Identificador do responsável</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorMessageTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorsResponseTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorTraceTO))]
        public async Task<IActionResult> DeleteAsync([FromRoute(Name = "id")][Required] Guid? id)
        {
            try
            {
                await _responsibleFacade.DeleteAsync(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.ErrorResponse);
            }
            catch (ErrorsException ex)
            {
                return BadRequest(ex.ErrorResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DeleteAsync", new object[] { id });
                return StatusCode((int)HttpStatusCode.InternalServerError, ErrorHelper.GenericError(ex));
            }

            return Ok();
        }
    }
}