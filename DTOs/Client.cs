using System.Collections.Generic;

namespace ReportGenerator.DTOs
{
    public class Client
    {
        public string Name { get; set; }
        public IEnumerable<JobDefinition> JobDefinition { get; set; }
        public IEnumerable<User> Users { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
