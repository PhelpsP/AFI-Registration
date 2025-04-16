using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Application
{
    public class CustomerDetailsDto
    {
        public string PolicyholderFirstName { get; set; } = string.Empty;

        public string PolicyholderSurname { get; set; } = string.Empty;

        public string PolicyReferenceNumber { get; set; } = string.Empty;

        public DateTime? PolicyholderDOB { get; set; }

        public string? PolicyholderEmail { get; set; }
    }
}
