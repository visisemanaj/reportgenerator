using System.Collections.Generic;

namespace ReportGenerator.DTOs
{
    public class ClientReportItem
    {
        public string Name { get; set; }

        public IList<JobDefinitionReportItem> JobDefinition { get; set; } = new List<JobDefinitionReportItem>();

        public decimal Total { get; set; }
    }
}
