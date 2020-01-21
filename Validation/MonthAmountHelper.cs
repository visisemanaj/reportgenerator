using System;
using System.Collections.Generic;
using System.Linq;
using ReportGenerator.DTOs;

namespace ReportGenerator.Validation
{
    public static class MonthAmountHelper
    {
        public static decimal GetAmount(this IEnumerable<MonthAmount> items, int month, string propertyName)
        {
            var percentageOfTime = items.OrderByDescending(x => x.GetMonthParsed())
                .FirstOrDefault(x => x.GetMonthParsed() <= month);

            if (percentageOfTime == null)
            {
                throw new Exception($"{new DateTime(2000, month, 1).ToString("MMMM")} is not defined in the {propertyName}.");
            }

            return percentageOfTime.Amount;
        }
    }
}
