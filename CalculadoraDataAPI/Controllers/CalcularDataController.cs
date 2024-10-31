using CalculadoraDataAPI.DTOs;
using CalculadoraDataAPI.Interfaces;
using CalculadoraDataAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace CalculadoraDataAPI.Controllers
{
    [Route("api/v2/calculardata")]
    [ApiController]
    public class CalcularDataController : ControllerBase
    {
        private readonly IDataCalculationService _dataCalculationService;
        private IConversorCalendarioService _conversorCalendario;

        public CalcularDataController(IDataCalculationService dataCalculationService, IConversorCalendarioService conversorCalendario)
        {
            _dataCalculationService = dataCalculationService;
            _conversorCalendario = conversorCalendario;
        }

        /// <summary>
        /// Calcula uma nova data com base na operação e valor fornecidos.
        /// </summary>
        /// <param name="request">Objeto contendo a data inicial, operação e valor.</param>
        /// <returns>Nova data calculada.</returns>
        /// <response code="200">Retorna a nova data calculada.</response>
        /// <response code="400">Se os parâmetros fornecidos são inválidos.</response>
        /// <response code="500">Se ocorreu um erro interno no servidor.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Calcula uma nova data",
            Description = "Calcula uma nova data com base na operação e valor fornecidos.",
            OperationId = "CalcularData",
            Tags = new[] { "CalcularData" }
        )]
        [SwaggerResponse(StatusCodes.Status200OK, "Retorna a nova data calculada.", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Se os parâmetros fornecidos são inválidos.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Se ocorreu um erro interno no servidor.")]
        public IActionResult Get([FromQuery] DateRequestDto request)
        {
            try
            {
                string date = _dataCalculationService.CalculateDate(request);
                return Ok(date);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro na sua solicitação. Por favor, tente mais tarde!");
            }
        }
    }
}
