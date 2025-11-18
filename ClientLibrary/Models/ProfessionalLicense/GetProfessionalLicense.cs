using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.ProfessionalLicense
{
    public class GetProfessionalLicense : BaseProfessionalLicense
    {
        public Guid Id { get; set; }
        public string ProfessionalId { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
