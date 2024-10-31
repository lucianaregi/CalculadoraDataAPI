using System;
using CalculadoraDataAPI.Interfaces;

namespace CalculadoraDataAPI.Services
{
    public class ConversorCalendarioService : IConversorCalendarioService
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;

        private delegate int Operation(int a, int b);

        public bool IsJulianDate(int year, int month, int day)
        {
            if (year < 1582) return true;
            if (year > 1582) return false;

            if (month < 10) return true;
            if (month > 10) return false;

            if (day < 5) return true;
            if (day > 14) return false;

            throw new ArgumentOutOfRangeException("Esta data não é válida, ela não existe nos calendários Gregoriano ou Julian.");
        }

        public double DateToJulian(int year, int month, int day, int hour, int minute, string operation, long alterDate)
        {
            bool isJulianCalendar = IsJulianDate(year, month, day);
            Operation selectedOperation = operation == "-" ? Subtract : Add;

            int adjustedMinutes = selectedOperation(minute, unchecked((int)alterDate));
            int adjustedMonth = month > 2 ? month : month + 12;
            int adjustedYear = month > 2 ? year : year - 1;
            double dayWithTime = day + hour / 24.0 + adjustedMinutes / 1440.0;

            int B = isJulianCalendar ? 0 : 2 - adjustedYear / 100 + adjustedYear / 100 / 4;
            return Math.Floor(365.25 * (adjustedYear + 4716)) + Math.Floor(30.6001 * (adjustedMonth + 1)) + dayWithTime + B - 1524.5;
        }

        public double JulianDate(int year, int month, int day, int hour, int minute, string operation, long alterDate)
        {
            return DateToJulian(year, month, day, hour, minute, operation, alterDate);
        }

        public string JulianToGregorian(double julianDate)
        {
            julianDate += 0.5;
            int julianDay = (int)Math.Floor(julianDate);
            double fractionalDay = julianDate - julianDay;

            int calendar = julianDay + 1524;
            int yearAdjust = (int)((calendar - 122.1) / 365.25);
            int dayOfYear = (int)(365.25 * yearAdjust);
            int monthAdjust = (int)((calendar - dayOfYear) / 30.6001);

            double day = calendar - dayOfYear - Math.Floor(30.6001 * monthAdjust) + fractionalDay;
            double hours = (day - Math.Floor(day)) * 24;
            double minutes = (hours - Math.Floor(hours)) * 60;

            int dayInt = (int)Math.Floor(day);
            int month = monthAdjust < 14 ? monthAdjust - 1 : monthAdjust - 13;
            int year = month > 2 ? yearAdjust - 4716 : yearAdjust - 4715;

            return $"{dayInt:D2}/{month:D2}/{year} {Math.Floor(hours):D2}:{Math.Floor(minutes):D2}";
        }
    }
}
