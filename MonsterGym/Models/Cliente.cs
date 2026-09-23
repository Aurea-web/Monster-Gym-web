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
    public string? ContactoEmergencia { get; set; }
    public bool Activo { get; set; } = true;
    public ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
