using System.Collections.Generic;
using ReportGenerator.Validation;

namespace ReportGenerator.DTOs
{
    public class JobDefinitionReportItem
    {
        public string Name { get; set; }

        public IEnumerable<MonthAmount> TotalHours { get; set; }

        public decimal GetTotalHoursAmount(int month)
        {
            return TotalHours.GetAmount(month, "TotalHours");
        }

        public IList<decimal> SubTotal { get; set; }

        public IList<UserReportItem> Users { get; set; } = new List<UserReportItem>();
    }
}
