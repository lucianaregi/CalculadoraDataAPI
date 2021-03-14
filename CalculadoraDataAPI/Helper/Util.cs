using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalculadoraDataAPI.Helper
{
    public static class Util
    {
        /// <summary>
        /// Valida a data no formato dd/mm/yyyy hh:mm
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool DateValidate(string date)
        {
            try
            {
                string pattern = @"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4} (2[0-3]|[01]?[0-9]):([0-5]?[0-9])$";
                Regex regex = new Regex(pattern);
                Match match = regex.Match(date);

                if (match.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Valida o valor
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValid(long value)
        {
            try
            {
                long _value = value;
                if (_value > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Retorna um valor positivo
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long PositiveNumber(long value)
        {
            long _value = value;
            if (_value < 0)
                _value = _value * (-1);

            return _value;
        }

        /// <summary>
        /// Valida o operador
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool OperatorValidate(string value)
        {
            try
            {
                if (value == "+" || value == "-")
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
