using CalculadoraDataAPI.DTOs;

namespace CalculadoraDataAPI.Interfaces
{

    public interface IDataCalculationService
    {
        string CalculateDate(DateRequestDto request);
    }
}
