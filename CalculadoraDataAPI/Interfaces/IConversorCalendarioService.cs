using CalculadoraDataAPI.DTOs;

namespace CalculadoraDataAPI.Interfaces
{
    public interface IConversorCalendarioService
    {
        double JulianDate(int year, int month, int day, int hour, int minute, string operation, long value);
        string JulianToGregorian(double julianDate);
    }

}
