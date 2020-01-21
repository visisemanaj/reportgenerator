using System;
using System.Collections.Generic;
using ReportGenerator.Validation;

namespace ReportGenerator.DTOs
{
    public class JobFunction
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<MonthAmount> PercentageOfTime { get; set; }

        public decimal GetPercentageOfTimeAmount(int month)
        {
            return PercentageOfTime.GetAmount(month, "PercentageOfTime");
        }

        public decimal CalcPercentageOfTime(decimal value, DateTime date)
        {
            return value * GetPercentageOfTimeAmount(date.Month) / 100m;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
