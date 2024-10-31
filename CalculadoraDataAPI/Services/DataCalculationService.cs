using CalculadoraDataAPI.Interfaces;
using CalculadoraDataAPI.DTOs;
using CalculadoraDataAPI.Utilities;
using System;

namespace CalculadoraDataAPI.Services
{
    public class DataCalculationService : IDataCalculationService
    {
        private readonly IConversorCalendarioService _conversorCalendario;

        public DataCalculationService(IConversorCalendarioService conversorCalendario)
        {
            _conversorCalendario = conversorCalendario;
        }

        public string CalculateDate(DateRequestDto request)
        {
            if (!DateHelper.DateValidate(request.Data) || request.Valor <= 0 || (request.Operacao != "+" && request.Operacao != "-"))
            {
                throw new ArgumentException("Dados inválidos!");
            }

            var valueDate = request.Data.Split('/');
            var day = int.Parse(valueDate[0]);
            var month = int.Parse(valueDate[1]);
            var year = int.Parse(valueDate[2].Split(' ')[0]);
            var hour = int.Parse(valueDate[2].Split(' ')[1].Split(':')[0]);
            var minute = int.Parse(valueDate[2].Split(' ')[1].Split(':')[1]);

            double julianDate = _conversorCalendario.JulianDate(year, month, day, hour, minute, request.Operacao, request.Valor);
            return _conversorCalendario.JulianToGregorian(julianDate);
        }
    }
}
