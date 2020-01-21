using System;
using System.Globalization;

namespace ReportGenerator.DTOs
{
    public class MonthAmount
    {
        public string Month { get; set; }

        public decimal Amount { get; set; }

        public int GetMonthParsed()
        {
            DateTime date;
            if (DateTime.TryParseExact(Month, "MMM", null, DateTimeStyles.None, out date) ||
                DateTime.TryParseExact(Month, "MMMM", null, DateTimeStyles.None, out date))
            {
                return date.Month;
            }

            throw new Exception($"Can't parse month {Month}.");
        }
    }
}
