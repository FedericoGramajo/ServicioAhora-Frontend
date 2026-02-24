using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Models.Authentication
{
    public class ChangePassword
    {
        public string Token { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "La contraseña obligatoria.")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Por favor, repita la contraseña."), Compare(nameof(Password))]
        public string? ConfirmPassword { get; set; }

    }
}
