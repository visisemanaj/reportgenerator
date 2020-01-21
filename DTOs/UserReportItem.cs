using System.Collections.Generic;

namespace ReportGenerator.DTOs
{
    public class UserReportItem
    {
        public string FriendlyName { get; set; }

        public IList<decimal> WeeksHours { get; set; } = new List<decimal>();
    }
}
