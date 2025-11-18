using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.ProfessionalCat
{
    public class BaseProfessionalCategory
    {
        public string ProfessionalId { get; set; } = default!;
        public Guid CategoryId { get; set; }

    }
}
