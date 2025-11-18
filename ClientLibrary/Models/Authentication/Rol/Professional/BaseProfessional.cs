using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.Authentication.Rol.Professional
{
    public class BaseProfessional
    {

        [Required]
        public string? AppUserId { get; set; }
    }
}
