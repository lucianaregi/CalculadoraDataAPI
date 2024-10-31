using System.Text.RegularExpressions;

namespace CalculadoraDataAPI.Utilities
{
    public static class DateHelper
    {
        public static bool DateValidate(string date)
        {
            string pattern = @"^(3[01]|[12][0-9]|0[1-9])/(1[0-2]|0[1-9])/[0-9]{4} (2[0-3]|[01]?[0-9]):([0-5]?[0-9])$";
            return Regex.IsMatch(date, pattern);
        }

        public static long PositiveNumber(long value)
        {
            return value < 0 ? -value : value;
        }
    }
}
