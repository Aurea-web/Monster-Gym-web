using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio")]
    [EmailAddress(ErrorMessage = "Correo no válido")]
    [Display(Name = "Correo electrónico")]
    public string Correo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Contrasena { get; set; } = string.Empty;
}