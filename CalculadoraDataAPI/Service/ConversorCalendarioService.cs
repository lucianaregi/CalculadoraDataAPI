using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculadoraDataAPI.Service
{
    public class ConversorCalendarioService
    {
        /// <summary>
        /// Operação de soma
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Add(int a, int b) { return a + b; }

        /// <summary>
        /// Operacão de subtração
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Subtract(int a, int b) { return a - b; }

        delegate int Operation(int a, int b);

        /// <summary>
        /// Verifica se a data existe nos calendários Julian e Gregoriano
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public bool IsJulianDate(int year, int month, int day)
        {
            // Todas as datas anterior a 1582 são do calendário Julian 
            if (year < 1582)
                return true;
            // Todas as datas posteriores a 1582 são do calendário Gregoriano
            else if (year > 1582)
                return false;
            else
            {
                // Se 1582, checa se é antes de 04 de Outubro (Julian) ou depois de 15 de Outubro (Gregoriano)
                if (month < 10)
                    return true;
                else if (month > 10)
                    return false;
                else
                {
                    if (day < 5)
                        return true;
                    else if (day > 14)
                        return false;
                    else
                        // Se a data estiver entre 10/5/1582 e 10/14/1582 é inválida 
                        throw new ArgumentOutOfRangeException(
                            "Esta data não é válida, ela não existe nos calendários Gregoriano ou Julian.");
                }
            }
        }
        /// <summary>
        /// Converte a data para o calendário Julian e faz a operação de soma ou subtração
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="op"></param>
        /// <param name="alterDate"></param>
        /// <returns></returns>
        public double DateToJD(int year, int month, int day, int hour, int minute, string op, long alterDate)
        {
            // Determina o calendário correto a partir da data
            bool JulianCalendar = IsJulianDate(year, month, day);

            Operation _operator = Add;
            if (op == "-")
            {
                _operator = Subtract;
            }

            int _alterDate = _operator(minute, unchecked((int)alterDate));


            int _month = month > 2 ? month : month + 12;
            int _year = month > 2 ? year : year - 1;
            double _day = day + hour / 24.0 + (_alterDate / 1440.0);

            int B = JulianCalendar ? 0 : 2 - _year / 100 + _year / 100 / 4;

            return (int)(365.25 * (_year + 4716)) + (int)(30.6001 * (_month + 1)) + _day + B - 1524.5;
        }

        public double JulianDate(int year, int month, int day, int hour, int minute, string alterDate, long plusMinutes)
        {
            return DateToJD(year, month, day, hour, minute, alterDate, plusMinutes);
        }

        /// <summary>
        /// Converte a data para o calendário gregoriano
        /// </summary>
        /// <param name="julianDate"></param>
        /// <returns></returns>
        public string JulianToGregorian(double julianDate)
        {
            julianDate += .5;
            string gregorianDate = string.Empty;
            int _julianDate = (int)Math.Floor(julianDate);
            double result = julianDate - _julianDate;
            int calendar = 0;
            int alpha = (int)((_julianDate - 1867216.25) / 36524.25);
            calendar = _julianDate + 1 + alpha - (int)(alpha / 4d);
            int calendarAjust = calendar + 1524;
            int yearAjust = (int)((calendarAjust - 122.1) / 365.25);

            int dayAjust = (int)(365.25 * yearAjust);
            int timeAjust = (int)((calendarAjust - dayAjust) / 30.6001);

            double day = calendarAjust - dayAjust - (int)(30.6001 * timeAjust) + result;
            double hour = day - (int)Math.Floor(day);
            hour *= 24;
            double minutes = hour - (int)(hour);
            minutes = minutes * 60;

            int mm = 0, yyyy = 0;

            if (timeAjust < 13.5)
            {
                mm = timeAjust - 1;
            }
            else
            {
                mm = timeAjust - 13;
            }
            if (mm > 2.5)
            {
                yyyy = yearAjust - 4716;
            }
            else
            {
                yyyy = yearAjust - 4715;
            }

            string _day = (int)day < 10 ? "0" + (int)day : ((int)day).ToString();
            string _mm = (int)mm < 10 ? "0" + (int)mm : ((int)mm).ToString();

            var dateFormat = _day + "/" + _mm + "/" + (int)yyyy + " " + (int)hour + ":" + (int)minutes;

            gregorianDate = dateFormat.ToString();

            return gregorianDate;
        }
    }
}
