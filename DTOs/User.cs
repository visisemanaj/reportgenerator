using System.Collections.Generic;

namespace ReportGenerator.DTOs
{
    public class User
    {
        public string Name { get; set; }

        public decimal ExpectedPercentageOfDailyHours { get; set; }

        public IEnumerable<JobFunction> JobFunctions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
