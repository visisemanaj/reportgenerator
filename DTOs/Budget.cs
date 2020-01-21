using System.Collections.Generic;

namespace ReportGenerator.DTOs
{
    public class Budget
    {
        public IEnumerable<Client> Clients { get; set; }
    }
}
