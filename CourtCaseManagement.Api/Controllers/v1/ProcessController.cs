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
    [Route("courtCaseManagement/v{version:apiVersion}/process")]
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

        /// <summary>
        /// Cadastrar processo
        /// </summary>
        /// <remarks>
        ///**Modelo de Dados** *(corpo da requisição, body)*
        ///|Parâmetro|||Obrigatório|Tipo|Tamanho|Descrição|
        ///|---|---|---|:---:|:---:|---|---|
        ///|**unifiedProcessNumber**|||✔️|string|20|<span>Número do processo unificado</span>|
        ///|**distributionDate**|||❌|date||<span>Data de distribuição.</span><br/><span>Formato: yyyy-MM-dd.</span>|
        ///|**justiceSecret**|||✔️|boolean||<span>Processo segredo de justiça</span>|
        ///|**clientPhysicalFolder**|||❌|string|50|<span>Pasta física cliente.</span>|
        ///|**description**|||❌|string|1000|<span>Descrição.</span>|
        ///|**situationId**|||✔️|guid||<span>Identificador da situação do processo.</span>|
        ///|**responsibles**|||✔️|||<span>Responsáveis.</span>|
        ///||**id**||✔️|guid||<span>Identificador do responsável.</span>|
        ///|**linkedProcessId**|||❌|guid||<span>Processo vinculado (processo pai).</span>|
        /// </remarks>
        /// <returns>ProcessResponseTO</returns>
        [HttpPost]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProcessResponseTO))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> AddAsync([FromBody][Required] ProcessRequestTO processRequestTO)
        {
            ProcessResponseTO responseTO = null;

            try
            {
                responseTO = await _processFacade.AddAsync(processRequestTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync", new object[] { processRequestTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Created(nameof(AddAsync), responseTO);
        }

        /// <summary>
        /// Consultar processo
        /// </summary>
        /// <remarks>
        ///|Parâmetro|||Obrigatório|Tipo|Tamanho|Descrição|
        ///|---|---|---|:---:|:---:|---|---|
        ///|**unifiedProcessNumber**|||❌|string|400|<span>Número de processo unificado (formatado e não formatado)</span>|
        ///|**distributionDateStart**|||❌|date||<span>Data de inicio da distribuição.</span><br/><span>Formato: **yyyy-MM-dd**.</span>|
        ///|**distributionDateEnd**|||❌|date||<span>Data fim da distribuição.</span><br/><span>Formato: **yyyy-MM-dd**.</span>|
        ///|**justiceSecret**|||❌|boolean||<span>Por processos em segredo de justiça</span>|
        ///|**clientPhysicalFolder**|||❌|string||<span>Por parte do texto da pasta física cliente.</span>|
        ///|**situationId**|||❌|guid||<span>Por uma determinada situação.</span>|
        ///|**responsibleName**|||❌|string||<span>Por parte do nome do responsável.</span>|
        ///|**page**|||❌|int||<span>Número da Página.</span><br/><span>**Valor Padrão**: 1</span>|
        ///|**perPage**|||❌|int||<span>Quantidade de registros por página.</span><br/><span>**Valor Padrão**: 10</span><br/><span>**Valor Máximo**: 50</span>|
        /// </remarks>
        /// <returns>ProcessResponseTO</returns>
        [HttpGet]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ProcessResponseTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> ListAsync([FromQuery] ProcessFilterTO filterTO)
        {
            IList<ProcessResponseTO> listResponseTO = null;

            try
            {
                listResponseTO = await _processFacade.ListAsync(filterTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AddAsync", new object[] { filterTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Ok(listResponseTO);
        }

        /// <summary>
        /// Editar processo
        /// </summary>
        /// <remarks>
        ///**Modelo de Dados** *(corpo da requisição, body)*
        ///|Parâmetro|||Obrigatório|Tipo|Tamanho|Descrição|
        ///|---|---|---|:---:|:---:|---|---|
        ///|**unifiedProcessNumber**|||❌|string|400|<span>Número do processo unificado</span>|
        ///|**distributionDate**|||❌|date||<span>Data de distribuição.</span><br/><span>Formato: yyyy-MM-dd.</span>|
        ///|**justiceSecret**|||❌|boolean||<span>Processo segredo de justiça</span>|
        ///|**clientPhysicalFolder**|||❌|string|400|<span>Pasta física cliente.</span>|
        ///|**description**|||❌|string|400|<span>Descrição.</span>|
        ///|**situationId**|||❌|guid||<span>Identificador da situação do processo.</span>|
        ///|**responsibles**|||❌|||<span>Responsáveis.</span>|
        ///||**id**||❌|guid||<span>Identificador do responsável.</span>|
        ///|**linkedProcessId**|||❌|guid||<span>Processo vinculado (processo pai).</span>|
        /// </remarks>
        /// <param name="id">Identificador do processo</param>
        /// <returns>ProcessResponseTO</returns>
        [HttpPut("{id}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Exception))]
        public async Task<IActionResult> UpdateAsync([FromRoute(Name = "id")][Required] Guid? id, [FromBody][Required] ProcessRequestTO processRequestTO)
        {
            try
            {
                await _processFacade.UpdateAsync(id, processRequestTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateAsync", new object[] { id, processRequestTO });
                return StatusCode((int)HttpStatusCode.InternalServerError, ex);
            }

            return Ok();
        }

        /// <summary>
        /// Remover processo
        /// </summary>
        /// <param name="id">Identificador do processo</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [MapToApiVersion("1")]
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