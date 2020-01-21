using System.Collections.Generic;
using ReportGenerator.Validation;

namespace ReportGenerator.DTOs
{
    public class JobDefinition
    {
        public string Name { get; set; }
        public IEnumerable<MonthAmount> TotalHours { get; set; }

        public decimal GetTotalHoursAmount(int month)
        {
            return TotalHours.GetAmount(month, "TotalHours");
        }
    }
}
