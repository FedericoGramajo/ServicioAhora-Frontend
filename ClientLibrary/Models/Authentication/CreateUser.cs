using System.ComponentModel.DataAnnotations;

namespace ClientLibrary.Models.Authentication
{
    public enum UserType { Customer = 0, Professional = 1 }
    public class CreateUser : AuthenticationBase
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w]).+$", ErrorMessage = "La contraseña debe contener mayúscula, minúscula, número y carácter especial.")]
        public new string? Password { get; set; }

        [Required(ErrorMessage = "Confirmar la contraseña es obligatorio.")]
        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        [StringLength(100, ErrorMessage = "Confirmar contraseña no debe exceder los 100 caracteres.")]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [Phone(ErrorMessage = "El formato del teléfono no es válido.")]
        public string? PhoneNumber { get; set; }

        public string? ProfileImage { get; set; }
        public required UserType UserType { get; set; }
    }
}
