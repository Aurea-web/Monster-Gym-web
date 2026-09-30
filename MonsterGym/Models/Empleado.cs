//using System.ComponentModel.DataAnnotations;

//namespace MonsterGym.Models;

//public class Empleado
//{
//    public int Id { get; set; }

//    [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
//    public string Nombre { get; set; } = string.Empty;

//    [StringLength(100, ErrorMessage = "El apellido no puede superar los 100 caracteres")]
//    public string Apellido { get; set; } = string.Empty;

//    [RegularExpression(@"^[0-9]{8,12}$", ErrorMessage = "El documento debe contener entre 8 y 12 dígitos")]
//    public string? Documento { get; set; }

//    [StringLength(15, ErrorMessage = "El teléfono no puede superar los 15 caracteres")]
//    public string? Telefono { get; set; }

//    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
//    public string? Correo { get; set; }

//    [Required(ErrorMessage = "La contraseña es obligatoria")]
//    [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
//    [DataType(DataType.Password)]
//    public string Contrasena { get; set; } = string.Empty;

//    [Required(ErrorMessage = "El cargo es obligatorio")]
//    public int CargoId { get; set; }

//    public bool Activo { get; set; } = true;

//    public Cargo? Cargo { get; set; }
//}

using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Empleado
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
    public string? Documento { get; set; }

    [Phone(ErrorMessage = "El número de teléfono no es válido")]
    [StringLength(15, ErrorMessage = "El teléfono no puede superar los 15 caracteres")]
    public string? Telefono { get; set; }

    [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
    public string? Correo { get; set; }

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = string.Empty;

    [Required(ErrorMessage = "El cargo es obligatorio")]
    public int CargoId { get; set; }

    public bool Activo { get; set; } = true;

    public Cargo? Cargo { get; set; }
}

