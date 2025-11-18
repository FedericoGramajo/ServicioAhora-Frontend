using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.Authentication.Rol.Custumer
{
    public class BaseCustumer
    {
        [Required]
        public string AppUserId { get; set; }
    }
}
