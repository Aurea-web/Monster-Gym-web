using System.ComponentModel.DataAnnotations;

namespace MonsterGym.Models;

public class Cliente
{
    public int Id { get; set; }
    [Required] public string Nombre { get; set; } = string.Empty;
    [Required] public string Apellido { get; set; } = string.Empty;
    [Required] public string Documento { get; set; } = string.Empty;
    [Phone] public string? Telefono { get; set; }
    [EmailAddress] public string? Correo { get; set; }
    [Required(ErrorMessage = "La contrasena es obligatoria")]
    [StringLength(255, MinimumLength = 6, ErrorMessage = "La contrasena debe tener al menos 6 characters")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = string.Empty;
    public string? ContactoEmergencia { get; set; }
    public bool Activo { get; set; } = true;
    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
