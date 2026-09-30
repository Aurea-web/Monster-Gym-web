//using System.ComponentModel.DataAnnotations;
//using System.Text.RegularExpressions;

//namespace MonsterGym.Models;

//public class Cliente
//{
//    public int Id { get; set; }


//    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
//    public string Nombre { get; set; } = string.Empty;


//    [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres")]
//    public string Apellido { get; set; } = string.Empty;


//    [RegularExpression(@"^[0-9]{8,12}$", ErrorMessage = "El documento debe contener entre 8 y 12 dígitos")]
//    public string Documento { get; set; } = string.Empty;

//    [Phone(ErrorMessage = "El número de teléfono no es válido")]
//    [StringLength(15, ErrorMessage = "El teléfono no puede superar los 15 caracteres")]
//    public string? Telefono { get; set; }

//    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
//    public string? Correo { get; set; }

//    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{6,}$",
//    ErrorMessage = "La contraseña debe incluir mayúsculas, minúsculas y números")]
//    [Required(ErrorMessage = "La contraseña es obligatoria")]
//    [StringLength(255, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
//    [DataType(DataType.Password)]
//    public string Contrasena { get; set; } = string.Empty;

//    [StringLength(100, ErrorMessage = "El contacto de emergencia no puede superar los 100 caracteres")]
//    public string? ContactoEmergencia { get; set; }

//    public bool Activo { get; set; } = true;

//    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
//}

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MonsterGym.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres")]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "El documento es obligatorio")]
    [RegularExpression(@"^[0-9]{8,12}$", ErrorMessage = "El documento debe contener entre 8 y 12 dígitos")]
    public string Documento { get; set; } = string.Empty;

    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    [StringLength(15, ErrorMessage = "El teléfono no puede superar los 15 caracteres")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string? Correo { get; set; }

    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{6,}$",
    ErrorMessage = "La contraseña debe incluir mayúsculas, minúsculas y números")]
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "El contacto de emergencia no puede superar los 100 caracteres")]
    public string? ContactoEmergencia { get; set; }

    public bool Activo { get; set; } = true;

    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}