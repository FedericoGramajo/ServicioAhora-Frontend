using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.ProfessionalCat
{
    public class GetProfessionalCategory : BaseProfessionalCategory
    {
        public string ProfessionalFullName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
    }
}
