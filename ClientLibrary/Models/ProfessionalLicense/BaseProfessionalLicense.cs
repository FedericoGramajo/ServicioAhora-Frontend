using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.ProfessionalLicense
{
    public class BaseProfessionalLicense
    {
        public string Number { get; set; } = string.Empty;
        public string? Issuer { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
