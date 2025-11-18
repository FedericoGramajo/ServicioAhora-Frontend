using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.ProfessionalLicense
{
    public class CreateProfessionalLicense : BaseProfessionalLicense
    {
        public Guid CategoryId { get; set; }
        public string ProfessionalId { get; set; } = default!;
    }
}
