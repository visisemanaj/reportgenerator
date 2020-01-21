using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.DTOs
{
    public class HourBurnReport
    {
        public List<DateTime[]> Weeks { get; set; }

        public IList<ClientReportItem> Clients { get; set; } = new List<ClientReportItem>();

        public IList<NameDecimal> UserExpectedAmount { get; set; } = new List<NameDecimal>();
    }
}
