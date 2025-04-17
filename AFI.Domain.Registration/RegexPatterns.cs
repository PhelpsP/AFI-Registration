using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AFI.Domain.Registration
{
    internal static class RegexPatterns
    {
        public static string PolicyReferencePattern = @"^[A-Z]{2}-\d{6}$";

        public static string EmailAddressPattern = @"^[a-zA-Z0-9]{4,}@[a-zA-Z0-9]{2,}\.(com|co\.uk)$";
    }
}
