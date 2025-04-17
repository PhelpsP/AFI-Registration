using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Data.Sqlite.Entities
{
    public class CustomerRecord
    {
        public int Id { get; set; }

        public required string FirstName { get; set; }

        public required string Surname { get; set; }

        public required string PolicyReferenceNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? Email { get; set; }
    }
}
