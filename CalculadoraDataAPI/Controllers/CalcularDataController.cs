using CalculadoraDataAPI.Helper;
using CalculadoraDataAPI.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculadoraDataAPI.Controllers
{
    [Route("api/v1/calculardata")]
    [ApiController]
    public class CalcularDataController : ControllerBase
    {
        private readonly ConversorCalendarioService _conversorCalendario;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="conversorCalendario"></param>
        public CalcularDataController(ConversorCalendarioService conversorCalendario) => _conversorCalendario = conversorCalendario;

        /// <summary>
        /// Método que retorna o cáclulo da data
        /// </summary>
        /// <param name="data">dd/mm/yyyy hh:mm</param>
        /// <param name="operacao">operador matemático soma (+) ou subtração (-)</param>
        /// <param name="valor"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] string data, [FromQuery] string operacao, [FromQuery] long valor)
        {
            string date = string.Empty;
            string message = string.Empty;
            bool _data = Util.DateValidate(data);
            valor = Util.PositiveNumber(valor);
            bool _valor = Util.IsValid(valor);
            bool _operacao = Util.OperatorValidate(operacao);

            try
            {
                if (_data && _valor && _operacao)
                {
                    var op = operacao;
                    var valueDate = data.Split('/');
                    var day = valueDate[0];
                    var month = valueDate[1];
                    var year = valueDate[2].Split(' ')[0];
                    var hour = valueDate[2].Split(' ')[1].Split(':')[0];
                    var minute = valueDate[2].Split(' ')[1].Split(':')[1];

                    double dateJulian = _conversorCalendario.JulianDate(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day), Int32.Parse(hour), Int32.Parse(minute), op, valor);
                    date = _conversorCalendario.JulianToGregorian(dateJulian);

                    return Ok(date);
                }
                else
                {
                    return BadRequest("Dados inválidos!");
                }
            }
            catch (Exception)
            {
                return BadRequest("Ocorreu um erro na sua solicitação. Por favor, tente mais tarde!");
            }
        }
    }
}
